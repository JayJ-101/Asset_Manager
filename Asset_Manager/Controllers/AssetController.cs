    using Asset_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.ProjectModel;

namespace Asset_Manager.Controllers
{
    public class AssetController : Controller
    {
        private Repository<Asset> assetData { get; set; }
        private Repository<Supplier> supplierData { get; set; }
        private Repository<Category> categoryData { get; set; }
  


        public AssetController(AssetDbContext ctx)
        {
            assetData = new Repository<Asset>(ctx);
            supplierData = new Repository<Supplier>(ctx);
            categoryData = new Repository<Category> (ctx);

        }

        public IActionResult Index(AssetGridData values)
        {
            var options = new QueryOptions<Asset>
            {
                Includes = "Category,Supplier",
                OrderByDirection = values.SortDirection,
                PageNumber = values.PageNumber,
                PageSize = values.PageSize,
            };

            if (values.IsSortByCategory)
                options.OrderBy = a => a.CategoryId;
            else
                options.OrderBy = a => a.AssetName;

            if (!string.IsNullOrEmpty(values.SearchQuery))
            {
                options.Where = a =>a.AssetName.Contains(values.SearchQuery) || a.Model.Contains(values.SearchQuery) ||
                    a.SerialNumber.Contains(values.SearchQuery); 
            }

            if (values.CategoryId.HasValue)
            {
                options.Where = a => a.CategoryId == values.CategoryId;
            }

           
            if (!string.IsNullOrEmpty(values.Status))
            {
                options.Where = a => a.Status == values.Status;
            }

            var vm = new AssetListViewModel
            {
                Assets = assetData.List(options),
                CurrentRoute = values,
                TotalPages = values.GetTotalPages(assetData.Count),
                Categories = categoryData.List(new QueryOptions<Category>
                {
                    OrderBy = c => c.CategoryName
                }),
                
                Statuses = new List<string> { "Available", "In Use", "Under Maintenance", "Retired" }
            };
        
            return View(vm);

        }
        // select (posted from genre drop down on Index page). 
        [HttpPost]
        public RedirectToActionResult Select(int id, string operation)
        {
            switch (operation.ToLower())
            {
                case "edit":
                    return RedirectToAction("Edit", new { id });
                case "delete":
                    return RedirectToAction("Delete", new { id });
                default:
                    return RedirectToAction("Index");
            }
        }

        #region Add
        [HttpGet]
        public ViewResult Add()
        {
            var vm = new AssetViewModel();
            LoadViewData(vm);
            return View("Asset", vm);
        }

        [HttpPost]
        public IActionResult Add(AssetViewModel vm, string operation)
        {
            var validate = new Validate(TempData);

            // Always validate on post
            validate.CheckAsset(vm.Asset.SerialNumber, operation, assetData);
            if (!validate.IsValid)
            {
                ModelState.AddModelError(nameof(vm.Asset.SerialNumber), validate.ErrorMessage);
            }

            if (ModelState.IsValid)
            {
                assetData.Insert(vm.Asset);
                assetData.Save();
                TempData["message"] = $"{vm.Asset.AssetName} added to Assets";
                return RedirectToAction("Index");
            }
            else
            {
                LoadViewData(vm);
                return View("Asset", vm);
            }
        }

        #endregion

        #region Edit
        [HttpGet]
        public ViewResult Edit(int id)
        {
            var vm = new AssetViewModel
            {
                Asset = GetAsset(id)
            };
            LoadViewData(vm);
            return View("Asset", vm);
        }

        [HttpPost]
        public IActionResult Edit(AssetViewModel vm)
        {
            if (ModelState.IsValid)
            {
              
                var asset = GetAsset(vm.Asset.AssetId);
                asset.Status = vm.Asset.Status;
                

                // don't need to call assetData.Update() - DB context is tracking   
                assetData.Save();

                TempData["message"] = $"{vm.Asset.AssetName} updated.";
                return RedirectToAction("Index");  // PRG pattern
            }
            else
            {
                LoadViewData(vm);
                return View("Asset", vm);
            }
        }
        #endregion

        #region Delete
        // delete
        [HttpGet]
        public ViewResult Delete(int id)
        {
            var vm = new AssetViewModel
            {
                Asset = assetData.Get(id) ?? new Asset()
            };
            return View("Asset", vm);
        }

        [HttpPost]
        public IActionResult Delete(Asset asset)
        {
            assetData.Delete(asset); // cascading delete will get BookAuthors
            assetData.Save();
            TempData["message"] = $"{asset.AssetName} removed from Books.";
            return RedirectToAction("Index");  // PRG pattern 
        }

        #endregion Delete


        public IActionResult Details(int id)
        {
            var asset = assetData.Get(new QueryOptions<Asset>
            {
                Where = a => a.AssetId == id,
                Includes = "Category,Supplier,AssetAssignments.Department,AssetAssignments.Branch"

            });

            if (asset == null) return NotFound();

            return View(asset);
        }


        #region Private Methods
        private Asset GetAsset(int id)
        {
            var options = new QueryOptions<Asset>
            {
                Where = a => a.AssetId == id,
                Includes = "Category,Supplier,AssetAssignments"
            };
            return assetData.Get(options) ?? new Asset();
        }

        private void LoadViewData(AssetViewModel vm)
        {
            vm.Categories = categoryData.List(new QueryOptions<Category>
            {
                OrderBy = c => c.CategoryName
            });
            vm.Suppliers = supplierData.List(new QueryOptions<Supplier>
            {
                OrderBy = c => c.SupplierName
            });
          
          
        }
        #endregion
    }
}

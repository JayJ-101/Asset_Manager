using Asset_Manager.Models;
using Microsoft.AspNetCore.Mvc;

namespace Asset_Manager.Controllers
{
    public class SupplierController : Controller
    {
        private Repository<Supplier> supplierData { get; set; }
        public SupplierController(AssetDbContext ctx) => supplierData = new Repository<Supplier>(ctx);

        public ActionResult Index(SupplierGridData values)
        {
            var options = new QueryOptions<Supplier>
            {
                OrderByDirection = values.SortDirection,
                PageNumber = values.PageNumber,
                PageSize = values.PageSize
            };
            //Sort By Supplier
            if (values.IsSortBySupplierName)
                options.OrderBy = s => s.SupplierName;

            if (!string.IsNullOrEmpty(values.SearchQuery))
                options.Where = s => s.SupplierName.Contains(values.SearchQuery) ||
                                     s.Email.Contains(values.SearchQuery);

            // View model
            var vm = new SupplierListViewModel
            {
                Suppliers = supplierData.List(options),
                CurrentRoute = values,
                TotalPages = values.GetTotalPages(supplierData.Count)
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

        // add
        [HttpGet]
        public ViewResult Add() => View("Supplier", new Supplier());

        [HttpPost]
        public IActionResult Add(Supplier supplier, string operation)
        {
            //server - side version of remote validation
            var validate = new Validate(TempData);
            if (!validate.IsSupplierChecked)
            {
                validate.CheckSupplier(supplier.SupplierName, operation, supplierData);
                if (!validate.IsValid)
                {
                    ModelState.AddModelError(nameof(supplier.SupplierName), validate.ErrorMessage);
                }
            }

            if (ModelState.IsValid)
            {
                supplierData.Insert(supplier);
                supplierData.Save();
                validate.ClearSupplier();
                TempData["message"] = $"{supplier.SupplierName} added to Suppliers.";
                return RedirectToAction("Index");  // PRG pattern
            }
            else
            {
                return View("Supplier", supplier);
            }
        }


        // edit
        [HttpGet]
        public ViewResult Edit(int id) => View("Supplier", supplierData.Get(id));

        [HttpPost]
        public IActionResult Edit(Supplier supplier)
        {
            // no remote validation of author on edit
            if (ModelState.IsValid)
            {
                supplierData.Update(supplier);
                supplierData.Save();
                TempData["message"] = $"{supplier.SupplierName} updated.";
                return RedirectToAction("Index");  // PRG pattern
            }
            else
            {
                return View("Supplier", supplier);
            }
        }
        // delete
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var supplier = supplierData.Get(new QueryOptions<Supplier>
            {
                Where = a => a.SupplierId == id
                //Includes = "Assets"
            });

            //if ( category != null && category.Assets.Count > 0)
            //{
            //    TempData["message"] = $"Can't delete category {category.CategoryName} " +
            //                          "because they have related Assets.";
            //    return RedirectToAction("Related", "Assets",
            //        new { id = "category-" + id });
            //}
            //else
            //{
            //    return View("Category", category);
            //}
            return View("Supplier", supplier);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Supplier supplier)
        {
            supplierData.Delete(supplier);
            supplierData.Save();
            TempData["message"] = $"{supplier.SupplierName} removed from Supplier.";
            return RedirectToAction("Index");  // PRG pattern
        }
    }
}

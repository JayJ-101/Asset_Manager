using Asset_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Asset_Manager.Controllers
{
    public class PurchaseOrderItemController : Controller
    {
        private Repository<PurchaseOrderItem> poItemData { get; set; }
        private Repository<Category> categoryData { get; set; }
        private Repository<PurchaseOrder> purchaseOrderData { get; set; }

        public PurchaseOrderItemController(AssetDbContext ctx)
        {
            poItemData = new Repository<PurchaseOrderItem>(ctx);
            categoryData = new Repository<Category>(ctx);
            purchaseOrderData = new Repository<PurchaseOrder>(ctx);
        }

        public IActionResult Index(PurchaseOrderItemGridData values)
        {
            var options = new QueryOptions<PurchaseOrderItem>()
            {
                Includes= "Category,PurchaseOrder",
                OrderByDirection = values.SortDirection,
                PageNumber = values.PageNumber,
                PageSize = values.PageSize,
            };
           
            if (!string.IsNullOrEmpty(values.SearchQuery))
            {
                options.Where = p => p.Category.CategoryName.Contains(values.SearchQuery);
            }

            if (values.CategoryId.HasValue)
            {
                options.Where = a => a.CategoryId == values.CategoryId;
            }

            var vm = new PurchaseOrderItemListViewModel
            {
                PurchaseOrderItem = poItemData.List(options),
                CurrentRoute = values,
                TotalPages = values.GetTotalPages(poItemData.Count),
                Categories = categoryData.List(new QueryOptions<Category>
                {
                    OrderBy = po => po.CategoryName
                }),
                Statuses = new List<string> { "Available", "In Use" }

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

        [HttpGet]
        public ViewResult Add()
        {
            var vm = new PurchaseOrderItemViewModel();
            LoadViewData(vm);
            return View("PurchaseOrderItem", vm);
        }
        [HttpPost]
        public IActionResult Add(PurchaseOrderItemViewModel vm)
        {
            // Server-side version of remote validation
            //var validate = new Validate(TempData);
            //if (!validate.IsAssetChecked)
            //{
            //    validate.CheckAsset(vm.Asset.SerialNumber, operation, assetData);
            //    if (!validate.IsValid)
            //    {
            //        ModelState.AddModelError(nameof(vm.Asset.SerialNumber), validate.ErrorMessage);
            //        Console.WriteLine($"Validation Error: {validate.ErrorMessage}");
            //    }
            //}

            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine("Model Error: " + error.ErrorMessage);
            }

            if (ModelState.IsValid)
            {
                poItemData.Insert(vm.PurchaseOrderItem);
                poItemData.Save();
                //poItemData.ClearAsset();
                TempData["message"] = $" Device is add and now available";
                return RedirectToAction("Index");
            }
            else
            {
               

                // Repopulate dropdown data if the model state is invalid
                LoadViewData(vm);
                return View("PurchaseOrderItem", vm); // Return the view with validation errors
            }
        }

        // GET: Edit
        [HttpGet]
        public ViewResult Edit(int id)
        {
            var vm = new PurchaseOrderItemViewModel
            {
                PurchaseOrderItem = poItemData.Get(id)
            };
            LoadViewData(vm);
            return View("PurchaseOrderItem", vm);
        }

        // POST: Edit
        [HttpPost]
        public IActionResult Edit(PurchaseOrderItemViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var order = GetPurchaseOrderItem(vm.PurchaseOrderItem.OrderITemId);
                order.CategoryId = vm.PurchaseOrderItem.CategoryId;
                order.Quantity = vm.PurchaseOrderItem.Quantity;
                order.UnitPrice = vm.PurchaseOrderItem.UnitPrice;
                poItemData.Save();
                TempData["message"] = $"Purchase Order updated.";
                return RedirectToAction("Index");
            }
            else
            {
                LoadViewData(vm);
                return View("PurchaseOrderItem", vm);
            }
        }

        // GET: Delete
        [HttpGet]
        public ViewResult Delete(int id)
        {
            var vm = new PurchaseOrderItemViewModel
            {
                PurchaseOrderItem = poItemData.Get(id) ?? new PurchaseOrderItem()
            };
            return View("PurchaseOrderItem", vm);
        }

        // POST: Delete
        [HttpPost]
        public IActionResult Delete(PurchaseOrderItem purchaseOrderItem)
        {
            poItemData.Delete(purchaseOrderItem);
            poItemData.Save();
            TempData["message"] = "Purchase Order Item deleted.";
            return RedirectToAction("Index");
        }


        private PurchaseOrderItem GetPurchaseOrderItem(int id)
        {
            var options = new QueryOptions<PurchaseOrderItem>
            {
                Where = p => p.OrderITemId == id,
                Includes = "PurchaseOrder, Category"
            };
            return poItemData.Get(options) ?? new PurchaseOrderItem();
        }
        private void LoadViewData(PurchaseOrderItemViewModel vm)
        {
            vm.Categories = categoryData.List(new QueryOptions<Category>
            {
                OrderBy = c => c.CategoryName
            });

            vm.PurchaseOrders = purchaseOrderData.List(new QueryOptions<PurchaseOrder>
            {
                OrderBy = p => p.PurchaseOrderId
            });
        }


        
    }
}

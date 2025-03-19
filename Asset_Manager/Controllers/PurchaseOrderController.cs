using Asset_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Asset_Manager.Controllers
{
    public class PurchaseOrderController : Controller
    {
        private Repository<PurchaseOrder> purchaseOrderData { get; set; }
        private Repository<Supplier> supplierData { get; set; }

        public PurchaseOrderController(AssetDbContext ctx)
        {
            purchaseOrderData = new Repository<PurchaseOrder>(ctx);
            supplierData = new Repository<Supplier>(ctx);
        }

        // Index - List all Purchase Orders
        public IActionResult Index(PurchaseOrderGridData values)
        {
            var options = new QueryOptions<PurchaseOrder>
            {
                Includes = "Supplier",
                OrderByDirection = values.SortDirection,
                PageNumber = values.PageNumber,
                PageSize = values.PageSize,
            };

            if (values.IsSortByDate)
                options.OrderBy = po => po.PurchaseDate;
            else
                options.OrderBy = po => po.Status;

            if (!string.IsNullOrEmpty(values.SearchQuery))
            {
                options.Where = po => po.Supplier.SupplierName.Contains(values.SearchQuery) ||
                                      po.Status.Contains(values.SearchQuery);
            }

            var vm = new PurchaseOrderListViewModel
            {
                PurchaseOrders = purchaseOrderData.List(options),
                CurrentRoute = values,
                TotalPages = values.GetTotalPages(purchaseOrderData.Count),
                Suppliers = supplierData.List(new QueryOptions<Supplier> {
                    OrderBy = s => s.SupplierName 
                }),
                Statuses = new List<string> { "Pending", "Recieved","Cancelled" }

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

        // GET: Add
        [HttpGet]
        public ViewResult Add()
        {
            var vm = new PurchaseOrderViewModel();
            LoadViewData(vm);
            return View("PurchaseOrder", vm);
        }

        // POST: Add
        [HttpPost]
        public IActionResult Add(PurchaseOrderViewModel vm, string operation)
        {
            // Server-side version of remote validation
            var validate = new Validate(TempData);
            if (!validate.IsAssetChecked)
            {
                validate.CheckPurchaseOrder(vm.PurchaseOrder.OrderNumber, operation, purchaseOrderData);
                if (!validate.IsValid)
                {
                    ModelState.AddModelError(nameof(vm.PurchaseOrder.OrderNumber), validate.ErrorMessage);
                    Console.WriteLine($"Validation Error: {validate.ErrorMessage}");
                }
            }
            if (ModelState.IsValid)
            {
                purchaseOrderData.Insert(vm.PurchaseOrder);
                purchaseOrderData.Save();
                validate.ClearOrder();
                TempData["message"] = $"Purchase Order for added.";
                return RedirectToAction("Index");
            }
            else
            {
                LoadViewData(vm);
                return View("PurchaseOrder", vm);
            }
        }

        // GET: Edit
        [HttpGet]
        public ViewResult Edit(int id)
        {
            var vm = new PurchaseOrderViewModel
            {
                PurchaseOrder = purchaseOrderData.Get(id)
            };
            LoadViewData(vm);
            return View("PurchaseOrder", vm);
        }

        // POST: Edit
        [HttpPost]
        public IActionResult Edit(PurchaseOrderViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var order = GetPurchaseOrder(vm.PurchaseOrder.PurchaseOrderId);
                order.Status = vm.PurchaseOrder.Status;
                order.ReceivedDate = vm.PurchaseOrder.ReceivedDate;
                purchaseOrderData.Save();
                TempData["message"] = $"Purchase Order updated.";
                return RedirectToAction("Index");
            }
            else
            {
                LoadViewData(vm);
                return View("PurchaseOrder", vm);
            }
        }

        // GET: Delete
        [HttpGet]
        public ViewResult Delete(int id)
        {
            var vm = new PurchaseOrderViewModel
            {
                PurchaseOrder = purchaseOrderData.Get(id) ?? new PurchaseOrder()
            };
            return View("PurchaseOrder", vm);
        }

        // POST: Delete
        [HttpPost]
        public IActionResult Delete(PurchaseOrder purchaseOrder)
        {
            purchaseOrderData.Delete(purchaseOrder);
            purchaseOrderData.Save();
            TempData["message"] = "Purchase Order deleted.";
            return RedirectToAction("Index");
        }


        private PurchaseOrder GetPurchaseOrder(int id)
        {
            var options = new QueryOptions<PurchaseOrder>
            {
                Where = p => p.PurchaseOrderId == id,
                Includes = "Supplier"
            };
            return purchaseOrderData.Get(options) ?? new PurchaseOrder();
        }
        private void LoadViewData(PurchaseOrderViewModel vm)
        {
            vm.Suppliers = supplierData.List(new QueryOptions<Supplier>
            {
                OrderBy = s => s.SupplierName
            });
        }
    }

}

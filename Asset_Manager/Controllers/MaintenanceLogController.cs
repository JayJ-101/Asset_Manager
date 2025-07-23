using Asset_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.ProjectModel;

namespace Asset_Manager.Controllers
{
    public class MaintenanceLogController : Controller
    {
        private Repository<MaintenanceLog> logData { get;set; }
        private Repository<Asset> assetData { get; set; }
        //private Repository<Category> Category { get; set; }


        public MaintenanceLogController(AssetDbContext ctx)
        {
            logData = new Repository<MaintenanceLog>(ctx);
            assetData = new Repository<Asset>(ctx);
        }

        public IActionResult Index(MaintenanceGriData values)
        {
            var options = new QueryOptions<MaintenanceLog>()
            {
                Includes = "Asset",
                OrderByDirection = values.SortDirection,
                PageSize = values.PageSize,
            };

            // Set default sort if not specified
            if (string.IsNullOrEmpty(values.SortField))
            {
                options.OrderBy = a => a.LoggedDate;
                options.OrderByDirection = "desc"; // Show newest first by default
            }
            else if (values.IsSortByAssetName)
            {
                options.OrderBy = a => a.Asset.AssetName;
            }

            // Apply filters only if they exist
            if (!string.IsNullOrEmpty(values.SearchQuery))
            {
                options.Where = a => a.Asset.AssetName.Contains(values.SearchQuery);
            }

            if (!string.IsNullOrEmpty(values.Status))
            {
                options.Where = a => a.Status == values.Status;
            }

            var vm = new MaintenanceLogListVM
            {
                MaintenanceLogs = logData.List(options),
                CurrentRoute = values,
                TotalPages = values.GetTotalPages(logData.Count),
                Statuses = new List<string> { "Pending", "Completed", "In Progress" }
            };
            // Debug output - remove after testing
            Console.WriteLine($"Total logs found: {vm.MaintenanceLogs.Count()}");
            foreach (var log in vm.MaintenanceLogs)
            {
                Console.WriteLine($"Log ID: {log.MaintenanceId}, Asset: {log.Asset?.AssetName}, Status: {log.Status}");
            }
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
        public IActionResult Add()
        {
            var vm = new MaintenanceLogViewModel
            {
                Assets = assetData.List(new QueryOptions<Asset> { OrderBy = a => a.AssetName })
            };
            return View("MaintenanceLog", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(MaintenanceLogViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Assets = assetData.List(new QueryOptions<Asset> { OrderBy = a => a.AssetName });
                return View("MaintenanceLog", vm);
            }

            logData.Insert(vm.MaintenanceLog);
            logData.Save();

            // Debug output
            var asset = assetData.Get(vm.MaintenanceLog.AssetId);
            Console.WriteLine($"Asset before update - ID: {asset?.AssetId}, Status: {asset?.Status}");

            if (asset != null)
            {
                asset.Status = "Under Maintenance";
                assetData.Update(asset);
                assetData.Save();
                Console.WriteLine($"Asset after update - ID: {asset.AssetId}, Status: {asset.Status}");
            }

            TempData["message"] = $"Maintenance logged for asset: {asset?.AssetName}.";
            return RedirectToAction("Index");
        }

        #endregion


        #region Completed Maintenance
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Complete(int id)
        {
            var log = logData.Get(id);
            if (log == null) return NotFound();

            log.Status = "Completed";
            log.CompletionDate = DateTime.Now;
            logData.Update(log);
            logData.Save();

            var asset = assetData.Get(log.AssetId);
            if (asset != null)
            {
                // You can use logic to check if it should go to Available or back to Assigned
                asset.Status = "Available"; // Or "Assigned" if reassigned
                assetData.Update(asset);
                assetData.Save();
            }

            TempData["message"] = "Maintenance completed and asset is now available.";
            return RedirectToAction("Index");
        }

        #endregion


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

        
        #endregion
    }
}

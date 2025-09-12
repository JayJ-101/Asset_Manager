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
                OrderByDirection = values.SortDirection ?? "desc", // Default to newest first
                PageSize = values.PageSize,
                OrderBy = a => a.LoggedDate // Default sort by LoggedDate
            };

            // Override default sort if sorting by asset name
            if (values.IsSortByAssetName)
            {
                options.OrderBy = a => a.Asset.AssetName;
            }

            // Apply filters
            if (!string.IsNullOrEmpty(values.SearchQuery))
            {
                options.Where = a => a.Asset.AssetName.Contains(values.SearchQuery);
            }

            if (!string.IsNullOrEmpty(values.Status))
            {
                options.Where = a => a.Status == values.Status;
            }

            //// DEBUG: Check the raw SQL being generated
            //var query = options.BuildQuery(logData.context.Set<MaintenanceLog>().AsQueryable());
            //Console.WriteLine(query.ToQueryString());

            var vm = new MaintenanceLogListVM
            {
                MaintenanceLogs = logData.List(options),
                CurrentRoute = values,
                TotalPages = values.GetTotalPages(logData.Count),
                Statuses = new List<string> { "Pending", "Completed", "In Progress" },
                Assets = assetData.List(new QueryOptions<Asset>())
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
        public IActionResult Add()
        {
            var vm = new MaintenanceLogViewModel
            {
                Assets = assetData.List(new QueryOptions<Asset> {
                    Where = a => a.Status == "Available",
                    OrderBy = a => a.AssetName 
                })

            };
            return View("MaintenanceLog", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(MaintenanceLogViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Assets = assetData.List(new QueryOptions<Asset> {
                    Where = a => a.Status == "Available",
                    OrderBy = a => a.AssetName 
                });

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
        [HttpPost]
        public IActionResult Complete(int id, string assetStatus = "Available")
        {
            var log = logData.Get(id);
            if (log == null) return NotFound();

            log.Status = "Completed";
            log.CompletionDate = DateTime.Now;
            logData.Update(log);

            var asset = assetData.Get(log.AssetId);
            if (asset != null)
            {
                asset.Status = assetStatus; // "Available" or "Assigned"
                assetData.Update(asset);
            }

            logData.Save();
            assetData.Save();

            TempData["message"] = $"Maintenance completed. Asset status set to {assetStatus}.";
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

using Asset_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.ProjectModel;

namespace Asset_Manager.Controllers
{
    public class MaintenanceLogController : Controller
    {
        private readonly Repository<MaintenanceLog> logData;
        private readonly Repository<Asset> assetData;

        public MaintenanceLogController(AssetDbContext ctx)
        {
            logData = new Repository<MaintenanceLog>(ctx);
            assetData = new Repository<Asset>(ctx);
        }

        public IActionResult Index(MaintenanceGriData values)
        {
            var options = new QueryOptions<MaintenanceLog>()
            {
                Includes= "Asset",
                OrderByDirection = values.SortDirection,
                PageSize = values.PageSize,
            };
  
            if (values.IsSortByAssetName)
            {
                options.OrderBy = a => a.AssetId;
            }

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
            return View();
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
    }
}

using Asset_Manager.Models;
using Asset_Manager.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;

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

        public IActionResult Index()
        {
            return View();
        }
    }
}

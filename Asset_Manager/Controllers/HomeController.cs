using Asset_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Asset_Manager.Controllers
{
    public class HomeController : Controller
    {
        private Repository<AssetAssignment> assignmentData { get; }
        private Repository<MaintenanceLog> maintenanceData { get; }
        private Repository<Asset> assetData { get; }

        public HomeController(AssetDbContext ctx)
        {
            assignmentData = new Repository<AssetAssignment>(ctx);
            assetData = new Repository<Asset>(ctx);
            maintenanceData = new Repository<MaintenanceLog>(ctx);    
         
        }

        public IActionResult Index()
        {
            var vm = new DashboardViewModel
            {
                TotalAssets = assetData.Count,
                AssignedAssets = assignmentData.List(new QueryOptions<AssetAssignment>()).Count(a => a.ReturnDate == null),

                UnderMaintenance = maintenanceData.List(new QueryOptions<MaintenanceLog>())
                        .Count(m => m.Status == "In Progress"),

                PendingReturns = assignmentData.List(new QueryOptions<AssetAssignment>())
                    .Count(a => a.ReturnDate == null && a.AssginedDate.AddDays(30) < DateTime.Now),

                RecentActivities = new List<string>
                {
                    "John Doe assigned Laptop X1 to IT Department",
                    "Laptop Z3 marked for maintenance",
                    "New supplier 'TechSource' added"
                }
            };

            return View(vm);
        }       
    }
}

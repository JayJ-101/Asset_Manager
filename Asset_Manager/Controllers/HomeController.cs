using Asset_Manager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace Asset_Manager.Controllers
{
    public class HomeController : Controller
    {
        private readonly Repository<AssetAssignment> _assignmentRepository;
        private readonly Repository<MaintenanceLog> _maintenanceRepository;
        private readonly Repository<Asset> _assetRepository;

        public HomeController(AssetDbContext ctx)
        {
            _assignmentRepository = new Repository<AssetAssignment>(ctx);
            _assetRepository = new Repository<Asset>(ctx);
            _maintenanceRepository = new Repository<MaintenanceLog>(ctx);
        }

        public IActionResult Index()
        {
            var now = DateTime.Now;
            var thirtyDaysAgo = now.AddDays(-30);
            var assignments = _assignmentRepository.List(new QueryOptions<AssetAssignment>());
            var assets = _assetRepository.List(new QueryOptions<Asset>());
            var maintenanceLogs = _maintenanceRepository.List(new QueryOptions<MaintenanceLog>());

            var vm = new DashboardViewModel
            {
                // Summary Stats
                TotalAssets = assets.Count(),
                AssignedAssets = assignments.Count(a => a.ReturnDate == null),
                UnderMaintenance = maintenanceLogs.Count(m => m.Status == "In Progress"),
                PendingReturns = assignments.Count(a => a.ReturnDate == null && a.AssginedDate.AddDays(30) < now),

                // Additional Metrics
                AvailableAssets = assets.Count(a => a.Status == "Available"),
                RetiredAssets = assets.Count(a => a.Status == "Retired"),
                AssetsAddedThisMonth = assets.Count(a => a.PurchaseDate >= thirtyDaysAgo),
               // AssignmentRate = assignments.Count(a => a.ReturnDate == null) / (decimal)assets.Count,
                OverdueReturns = assignments.Count(a => a.ReturnDate == null && a.AssginedDate.AddDays(45) < now),
                MaintenanceCompletedToday = maintenanceLogs.Count(m =>
                    m.CompletionDate.HasValue &&
                    m.CompletionDate.Value.Date == now.Date),

                // Recent Activities
                RecentActivities = GetRecentActivities(assignments, maintenanceLogs, now),
            };

            return View(vm);
        }

        private List<ActivityViewModel> GetRecentActivities(
            IEnumerable<AssetAssignment> assignments,
            IEnumerable<MaintenanceLog> maintenanceLogs,
            DateTime now)
        {
            var recentActivities = new List<ActivityViewModel>();

            // Get recent assignments
            var recentAssignments = assignments
                .Where(a => a.AssginedDate >= now.AddDays(-7))
                .OrderByDescending(a => a.AssginedDate)
                .Take(5);

            foreach (var assignment in recentAssignments)
            {
                recentActivities.Add(new ActivityViewModel
                {
                    Action = "Asset Assigned",
                    Description = $"{assignment.Asset.AssetName} assigned to {assignment.Employee}",
                    Timestamp = assignment.AssginedDate,
                    AssetName = assignment.Asset.AssetName,
                    StatusColor = "success",
                   // User = assignment.AssetName
                });
            }

            // Get recent maintenance
            var recentMaintenance = maintenanceLogs
                .Where(m => m.LoggedDate >= now.AddDays(-7))
                .OrderByDescending(m => m.LoggedDate)
                .Take(5);

            foreach (var maintenance in recentMaintenance)
            {
                recentActivities.Add(new ActivityViewModel
                {
                    Action = "Maintenance Logged",
                    Description = $"{maintenance.Asset.AssetName} - {maintenance.Description}",
                    Timestamp = maintenance.LoggedDate,
                    AssetName = maintenance.Asset.AssetName,
                    StatusColor = maintenance.Status == "Completed" ? "success" : "warning",
                  //  User = maintenance.
                });
            }

            return recentActivities
                .OrderByDescending(a => a.Timestamp)
                .Take(5)
                .ToList();
        }

        private List<MaintenanceDashboardViewModel> GetMaintenanceItems(IEnumerable<MaintenanceLog> logs)
        {
            var now = DateTime.Now;

            return logs
                .Where(m => m.Status != "Completed") // Only show active items
                .Select(m => new MaintenanceDashboardViewModel
                {
                    MaintenanceId = m.MaintenanceId,
                    AssetId = m.AssetId,
                    AssetName = m.Asset?.AssetName ?? "N/A", // Handle null asset
                    Status = m.Status,
                    DueDate = m.LoggedDate,
                    IsOverdue = m.Status == "Pending" && (now - m.LoggedDate).TotalDays > 7
                })
                .ToList();
        }
    }
}


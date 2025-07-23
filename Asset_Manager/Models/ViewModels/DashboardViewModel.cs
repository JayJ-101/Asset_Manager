namespace Asset_Manager.Models
{
    public class DashboardViewModel
    {
        public int TotalAssets { get; set; }
        public int AssignedAssets { get; set; }
        public int UnderMaintenance { get; set; }
        public int PendingReturns { get; set; }

        // Additional Metrics
        public int AvailableAssets { get; set; }
        public int RetiredAssets { get; set; }
        public int AssetsAddedThisMonth { get; set; }
        public int OverdueReturns { get; set; }
        public int MaintenanceCompletedToday { get; set; }
        public decimal AssignmentRate { get; set; } // Percentage (0-1)

        // Activity Feed
        public List<ActivityViewModel> RecentActivities { get; set; } = new();

        // Maintenance
        public List<MaintenanceDashboardViewModel> UpcomingMaintenance { get; set; } = new();
    }
}

namespace Asset_Manager.Models  
{
    public class MaintenanceDashboardVM
    {
        // Maintenance Metrics
        public int PendingCount { get; set; }
        public int InProgressCount { get; set; }
        public int MaintenanceCompletedToday { get; set; }

        // Asset Status
        public int TotalAssets { get; set; }
        public int AssetsUnderMaintenance { get; set; }
        public int AvailableAssets { get; set; }

        // Detailed Lists
        public List<MaintenanceLog> OverdueMaintenance { get; set; } = new();
        public List<MaintenanceActivityVM> RecentActivities { get; set; } = new();
    }
    public class MaintenanceActivityVM
    {
        public string AssetName { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public string Technician { get; set; }
    }
}

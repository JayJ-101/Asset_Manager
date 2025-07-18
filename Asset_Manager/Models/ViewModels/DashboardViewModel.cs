namespace Asset_Manager.Models
{
    public class DashboardViewModel
    {
        public int TotalAssets { get; set; }
        public int AssignedAssets { get; set; }
        public int UnderMaintenance { get; set; }
        public int PendingReturns { get; set; }

        public List<string> RecentActivities { get; set; } = new();
    }
}

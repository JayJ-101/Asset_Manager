namespace Asset_Manager.Models  
{
    public class MaintenanceLogListVM
    {
        public IEnumerable<MaintenanceLog> MaintenanceLogs { get; set; } = new List<MaintenanceLog>();
        public MaintenanceGriData CurrentRoute { get; set; } = new MaintenanceGriData();
        public int TotalPages { get; set; }

        public IEnumerable<string> Statuses { get; set; } = new List<string>
        {
           "Pending", "Completed", "In Progress"
        };
    }
}

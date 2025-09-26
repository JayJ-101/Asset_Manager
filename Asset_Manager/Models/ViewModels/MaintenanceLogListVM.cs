namespace Asset_Manager.Models  
{
    public class MaintenanceLogListVM
    {
        public IEnumerable<MaintenanceLog> MaintenanceLogs { get; set; } = new List<MaintenanceLog>();
        public IEnumerable<Asset> Assets { get; set; } = new List<Asset>();


        public MaintenanceGriData CurrentRoute { get; set; } = new MaintenanceGriData();
        public int TotalPages { get; set; }

        public IEnumerable<string> Statuses { get; set; } = new List<string>
        {
           "Under Maintenance", "Completed", 
        };

        //Counters 
        public int TotalAssets => Assets.Count();
        public int Pending => MaintenanceLogs.Count(a => a.Status == "Pending");
        public int CompletedCount => MaintenanceLogs.Count(a => a.Status == "Completed");
        public int ProgressCount => MaintenanceLogs.Count(a => a.Status == "In Progress");
        
    }
}

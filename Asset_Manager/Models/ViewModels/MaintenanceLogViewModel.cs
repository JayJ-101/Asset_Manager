namespace Asset_Manager.Models
{
    public class MaintenanceLogViewModel
    {
        public MaintenanceLog MaintenanceLog { get; set; } = new MaintenanceLog();
        public IEnumerable<Asset> Assets { get; set; } = new List<Asset>();
        public List<string> Statuses { get; set; } = new() { "Pending", "In Progress", "Completed" };
    }
}

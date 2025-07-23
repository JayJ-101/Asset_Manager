namespace Asset_Manager.Models
{
    public class MaintenanceDashboardViewModel
    {
        public int MaintenanceId { get; set; }  // From MaintenanceLog.MaintenanceId
        public int AssetId { get; set; }        // From MaintenanceLog.AssetId
        public string AssetName { get; set; }   // From MaintenanceLog.Asset.Name
        public string Status { get; set; }      // Directly from MaintenanceLog.Status ("Pending"/"In Progress"/"Completed")
        public DateTime DueDate { get; set; }   // Using LoggedDate as the due date
        public bool IsOverdue { get; set; }     // True if Pending and older than 7 days
    }
}

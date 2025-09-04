namespace Asset_Manager.Models  
{
    public class AssetListViewModel
    {
        public IEnumerable<Asset> Assets { get; set; } = new List<Asset>();
        public AssetGridData CurrentRoute { get; set; } = new AssetGridData();
        public int TotalPages {  get; set; }


        // Dropdown Data
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public IEnumerable<Branch> Branches { get; set; } = new List<Branch>();
        public IEnumerable<string> Statuses { get; set; } = new List<string>
        {
            "Available", "Under Maintenance" ,"Decomissioned"
        };

        //Counters 
        public int TotalAssets => Assets.Count();
        public int AvailableCount => Assets.Count(a => a.Status == "Available");
        public int MaintenanceCount => Assets.Count(a => a.Status == "Under Maintenance");
        public int AssignedCount => Assets.Count(a => a.Status == "Assigned");
        public int DecomissionedCount => Assets.Count(a => a.Status == "Decomissioned");
    }
}

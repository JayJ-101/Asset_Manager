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
            "Available", "In Use", "Under Maintenance" ,"Decomissioned"
        };
    }
}

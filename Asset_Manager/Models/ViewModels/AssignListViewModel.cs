using Microsoft.Identity.Client;
using NuGet.ContentModel;

namespace Asset_Manager.Models  
{
    public class AssignListViewModel
    {
        public IEnumerable<AssetAssignment> Assign { get; set; } = new List<AssetAssignment>();
        public IEnumerable<Asset> Asset { get; set; } = new List<Asset>();    

        public AssignGridData CurrentRoute {  get; set; } = new AssignGridData();
        public int TotalPages { get; set; }

        public IEnumerable<Branch> Branches { get; set; } = new List<Branch>(); 
        public IEnumerable<Department> Departments { get; set; } = new List<Department>();

        //Counters 
        public int ActiveAssingment => Assign.Count(a => !a.ReturnDate.HasValue);

        public int TotalAsset => Asset.Count();

        public int AvailableCount => Asset.Count(a => a.Status == "Available");

        public int MaintenanceCount => Asset.Count(a => a.Status == "Under Maintenance");

        public int DecomissionedCount => Asset.Count(a => a.Status == "Decomissioned");




    }
}

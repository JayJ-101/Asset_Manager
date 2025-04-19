using Microsoft.Identity.Client;

namespace Asset_Manager.Models  
{
    public class AssignListViewModel
    {
        public IEnumerable<AssetAssignment> Assign { get; set; } = new List<AssetAssignment>();    

        public AssignGridData CurrentRoute {  get; set; } = new AssignGridData();
        public int TotalPages { get; set; }

        public IEnumerable<Branch> Branches { get; set; } = new List<Branch>(); 
    }
}

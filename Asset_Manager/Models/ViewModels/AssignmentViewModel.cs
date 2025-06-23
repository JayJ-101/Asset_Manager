namespace Asset_Manager.Models
{
    public class AssignmentViewModel
    {
        public AssetAssignment AssetAssignment { get; set; } = new AssetAssignment();

        public IEnumerable<Branch> Branches { get; set; } = new List<Branch>();
        public IEnumerable<Department> Departments { get; set; } = new List<Department>();
        public IEnumerable<Asset> Assets { get; set; } = new List<Asset>();


    }
}

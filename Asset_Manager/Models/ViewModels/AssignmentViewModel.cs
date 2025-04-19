namespace Asset_Manager.Models
{
    public class AssignmentViewModel
    {
        public AssetAssignment AssetAssignment { get; set; } = new AssetAssignment();

        public IEnumerable<Branch> Branches { get; set; } = new List<Branch>();
    }
}

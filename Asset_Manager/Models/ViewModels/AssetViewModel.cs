namespace Asset_Manager.Models  
{
    public class AssetViewModel
    {
        public Asset Asset { get; set;} =new Asset();

       
        public string Employee { get; set; } = string.Empty;

        public int BranchId { get; set; }

        public IEnumerable<Category> Categories { get; set;} = new List<Category>();  
        public IEnumerable<Supplier> Suppliers { get; set;} = new List<Supplier>();
        public IEnumerable<Branch> Branches { get; set; } = new List<Branch>();

    }
}

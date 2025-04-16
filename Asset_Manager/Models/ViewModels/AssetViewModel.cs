namespace Asset_Manager.Models  
{
    public class AssetViewModel
    {
        public Asset Asset { get; set;} =new Asset();

        public IEnumerable<Category> Categories { get; set;} = new List<Category>();  
        public IEnumerable<Supplier> Suppliers { get; set;} = new List<Supplier>();

    }
}

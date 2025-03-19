namespace Asset_Manager.Models
{
    public class SupplierListViewModel
    {
        public IEnumerable<Supplier> Suppliers { get; set; } = new List<Supplier>();
        public SupplierGridData CurrentRoute { get; set; } = new SupplierGridData();
        public int TotalPages { get; set; }
    }
}

namespace Asset_Manager.Models  
{
    public class PurchaseOrderViewModel
    {
        public PurchaseOrder PurchaseOrder { get; set; } = new PurchaseOrder();
        
        public IEnumerable<Supplier> Suppliers { get; set; }= new List<Supplier>();
    }
}

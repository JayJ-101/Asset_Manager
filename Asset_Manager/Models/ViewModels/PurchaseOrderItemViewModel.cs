namespace Asset_Manager.Models  
{
    public class PurchaseOrderItemViewModel
    {
        public PurchaseOrderItem PurchaseOrderItem { get; set; } = new PurchaseOrderItem();


        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public IEnumerable<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
       
    }
}

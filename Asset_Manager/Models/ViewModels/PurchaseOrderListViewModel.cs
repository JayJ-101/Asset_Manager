namespace Asset_Manager.Models  
{
    public class PurchaseOrderListViewModel
    {
        public IEnumerable<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
        public PurchaseOrderGridData CurrentRoute { get; set; } = new PurchaseOrderGridData();
        public int TotalPages { get; set; }

        public IEnumerable<Supplier> Suppliers { get; set; } = new List<Supplier>();
        public IEnumerable<string> Statuses { get; set; } = new List<string>
        {
            "Pending", "Recieved"
        };

    }
}

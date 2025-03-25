namespace Asset_Manager.Models
{
    public class PurchaseOrderItemListViewModel
    {
        public IEnumerable<PurchaseOrderItem> PurchaseOrderItem { get; set; } = new List<PurchaseOrderItem>();
        public PurchaseOrderItemGridData CurrentRoute { get; set; } = new PurchaseOrderItemGridData();
        public int TotalPages { get; set; }

        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public IEnumerable<string> Statuses { get; set; } = new List<string>
        {
            "Available", "In Use"
        };

    }
}

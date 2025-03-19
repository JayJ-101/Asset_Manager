using System.Text.Json.Serialization;

namespace Asset_Manager.Models  
{
    public class PurchaseOrderGridData :GridData
    {
        public PurchaseOrderGridData() => SortField = nameof(PurchaseOrder.PurchaseOrderId);

        [JsonIgnore]
        public bool IsSortByDate =>
            SortField.Equals(nameof(PurchaseOrder.PurchaseDate));
        
        [JsonIgnore]
        public bool IsSortBySupplier =>
            SortField.Equals(nameof(PurchaseOrder.Supplier.SupplierName));
       
        [JsonIgnore]
        public bool IsSortByOrderNumber =>
            SortField.Equals(nameof(PurchaseOrder.OrderNumber));

        // Search Functionality
        public string SearchQuery { get; set; }

        // Filter Properties
        public int? SupplierId { get; set; }
        public string Status { get; set; }

    }
}

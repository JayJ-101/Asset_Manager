using System.Text.Json.Serialization;

namespace Asset_Manager.Models  
{
    public class PurchaseOrderItemGridData : GridData
    {
        public PurchaseOrderItemGridData() => SortField = nameof(PurchaseOrderItem.OrderITemId);

        [JsonIgnore]
        public bool IsSortByCategory =>
            SortField.Equals(nameof(PurchaseOrderItem.CategoryId));


        // Search Functionality
        public string SearchQuery { get; set; }

        // Filter Properties
        public string Status { get; set; }
        public int? CategoryId {  get; set; }  

    }
}
 
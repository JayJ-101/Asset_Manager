
using System.Text.Json.Serialization;

namespace Asset_Manager.Models  
{
    public class SupplierGridData : GridData
    {
        public SupplierGridData() => SortField = nameof(Supplier.SupplierName);

        //Sort flags
        [JsonIgnore]
        public bool IsSortBySupplierName =>
            SortField.EqualsNoCase(nameof(Supplier.SupplierName));


        //// Filtering by Category
        //public string CategoryId { get; set; }

        // Search functionality
        public string SearchQuery { get; set; }
    }
}

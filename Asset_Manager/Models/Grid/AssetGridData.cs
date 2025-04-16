using System.Text.Json.Serialization;

namespace Asset_Manager.Models
{
    public class AssetGridData : GridData
    {
        public AssetGridData() => SortField = nameof(Asset.AssetName);

        // Sort flags
        [JsonIgnore]
        public bool IsSortByCategory =>
            SortField.EqualsNoCase(nameof(Asset.CategoryId));

       

        [JsonIgnore]
        public bool IsSortByAssetName =>
            SortField.EqualsNoCase(nameof(Asset.AssetName));

            // Search Functionality
            public string SearchQuery { get; set; }

        // Filter Properties
        public int? CategoryId { get; set; }
       
        public string Status { get; set; }
    }
}

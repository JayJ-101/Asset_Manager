using System.Text.Json.Serialization;

namespace Asset_Manager.Models  
{
    public class MaintenanceGriData:GridData
    {
        public MaintenanceGriData() => SortField = nameof(Asset.AssetName);

        [JsonIgnore]
        public bool IsSortByAssetName =>
           SortField.EqualsNoCase(nameof(Asset.AssetName));

        // Search Functionality
        public string SearchQuery { get; set; }

        public string Status { get; set; }
    }
}

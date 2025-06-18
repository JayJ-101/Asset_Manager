using System.Text.Json.Serialization;

namespace Asset_Manager.Models  
{
    public class AssignGridData : GridData
    {
        public AssignGridData() => SortField = nameof(AssetAssignment.AssingmentId);

        // Sort flags
        [JsonIgnore]
        public bool IsSortByBranch =>
            SortField.EqualsNoCase(nameof(AssetAssignment.BranchId));
        
        //[JsonIgnore]
        //public bool IsSortByAssetName =>
        //   SortField.EqualsNoCase(nameof(AssetAssignment.AssetName));

        // Search Functionality
        public string SearchQuery { get; set; }
        // Filter Properties
        public int? BranchId { get; set; }

        //public string Status { get; set; }
    }

}

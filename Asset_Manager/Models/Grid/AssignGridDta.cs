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

       

        // Search Functionality
        public string SearchQuery { get; set; }
        
        // Filter Properties
        public int? BranchId { get; set; }
        public int? DepartmentId { get; set; }
        public int? CategoryId { get; set; }

        public bool ShowAll { get; set; } = false; // Default to false (show only active)
    }
}



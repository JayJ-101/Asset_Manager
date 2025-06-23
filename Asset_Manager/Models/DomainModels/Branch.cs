using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Asset_Manager.Models  
{
    public class Branch
    {
        [Key]
        public int BranchId { get; set; }

        [Required(ErrorMessage = "Branch name is required.")]
        [Remote("CheckBranch","Validation")]
        public string BranchName { get; set; } = string.Empty;

        public ICollection<Asset> Assets { get; set; } = new List<Asset>();
        public ICollection<Department> Departments { get; set; } = new List<Department>();
        public ICollection<AssetAssignment> AssetAssignments { get; set; } = new List<AssetAssignment>();

    }
}

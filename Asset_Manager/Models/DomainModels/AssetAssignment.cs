using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Asset_Manager.Models  
{
    public class AssetAssignment
    {
        [Key]
        public int AssingmentId { get; set; } 
        public int AssetId { get; set; }
        [ValidateNever]
        public Asset Asset { get; set; } = null!;

        [Required(ErrorMessage = "Employee name requried.")]
        public string Employee {  get; set; }   = string.Empty;

        public int BranchId { get; set; }
        [ValidateNever]
        public Branch Branch { get; set; } = null!;

        public DateTime AssginedDate { get; set; }
        public DateTime? ReturnDate { get; set; }


        public string? Notes { get; set; }
    }
}

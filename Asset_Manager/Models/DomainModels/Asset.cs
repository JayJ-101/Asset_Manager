using Asset_Manager.Models.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Asset_Manager.Models  
{
    public class Asset
    {
        [Key]
        public int AssetId { get; set; }

        [Required(ErrorMessage = "Manufacturer is required.")]
        [StringLength(30)]
        public string  Manufacturer { get; set; } =string.Empty;

        [Required(ErrorMessage = "Model name is required.")]
        [StringLength(30)]
        public string Model { get; set; } = string.Empty;

        [Required(ErrorMessage = "Name Required")]
        [StringLength(20)]
        public string AssetName { get; set; } =string.Empty;

        [Required(ErrorMessage = "Serial Number Required")]
        [Remote("CheckAsset", "Validation")]
        public string SerialNumber { get; set; } = string.Empty;

        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; } = null!;

        [Required]
        public int SupplierId { get; set; }
        [ValidateNever]
        public Supplier Supplier { get; set; } = null!;

        public string Status { get; set; } = "Available";
         
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Warranty Expiry Date is required.")]
        [WarrantyExpiryDateValidation(ErrorMessage = "Warranty Expiry Date must be in the future.")]
        public DateTime WarrantyExpiryDate { get; set; } = DateTime.Now.AddYears(2);

        public ICollection<AssetAssignment> AssetAssignments { get; set; } = new List<AssetAssignment>();
        public ICollection<MaintenanceLog> MaintenanceLogs { get; set; } = new List<MaintenanceLog>();
    }
}

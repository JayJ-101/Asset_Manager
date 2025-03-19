using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Asset_Manager.Models  
{
    public class Asset
    {
        [Key]
        public int AssetId { get; set; }

        [Required(ErrorMessage = "Name Required")]
        [StringLength(20)]
        public string AssetName { get; set; } =string.Empty;

        [Required(ErrorMessage = "Serial Number Required")]
        public string SerialNumber { get; set; } = string.Empty;

        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; } = null!;

        public int BranchId { get; set; }
        [ValidateNever]
        public Branch Branch { get; set; } = null!; 

        public string Status { get; set; } = "Available";

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Purchase Date is required.")]
        public DateTime PurchaseDate { get; set; } = DateTime.Now;
         
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Warranty Expiry Date is required.")]
        [WarrantyExpiryDateValidation(ErrorMessage = "Warranty Expiry Date must be in the future.")]
        public DateTime WarrantyExpiryDate { get; set; } = DateTime.Now.AddYears(2);

        [Required]
        public int PurchaseOrderId { get; set; }
        // Navigation property for PurchaseOrders (Many-to-Many)
        public PurchaseOrder PurchaseOrder { get; set; } = null!;

    }
}

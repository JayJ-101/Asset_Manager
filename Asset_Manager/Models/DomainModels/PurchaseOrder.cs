using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Asset_Manager.Models  
{
    public class PurchaseOrder
    {
        [Key]
        public int PurchaseOrderId { get; set; }

        [Required(ErrorMessage = "Order number is required.")]
        public string OrderNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Purchase date is required.")]
        [DataType(DataType.Date)]
        public DateTime PurchaseDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        public DateTime? ReceivedDate { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [StringLength(50, ErrorMessage = "Status cannot exceed 50 characters.")]
        public string Status { get; set; } = "Pending";

        // Foreign Key for Supplier
        [Required(ErrorMessage = "Please select a supplier.")]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Please add notes about order.")]
        public string Notes { get; set; } = string.Empty;
        
        [ValidateNever]
        public Supplier Supplier { get; set; } = null!;


        public ICollection<Asset> Assets { get; set; } = new List<Asset>();
    }
}

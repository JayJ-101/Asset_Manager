using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Asset_Manager.Models
{
    public class PurchaseOrderItem
    {
        [Key]
        public int OrderITemId { get; set; }

        public int PurchaseOrderId { get; set; }
        [ValidateNever]
        public PurchaseOrder PurchaseOrder { get; set; } = null!;

        public int CategoryId { get; set; }
        [ValidateNever]
        public Category Category { get; set; } = null!;

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit price must be greater than 0.")]
        public decimal UnitPrice { get; set; }

        public decimal TotalPrice => Quantity * UnitPrice;
    }

}

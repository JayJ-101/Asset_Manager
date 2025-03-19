using System.ComponentModel.DataAnnotations;

namespace Asset_Manager.Models  
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        [Required, StringLength(200)]
        public string SupplierName { get; set; } =string.Empty;

        [Required, StringLength(100)]
        public string ContactPerson { get; set; } = string.Empty;

        [Required, Phone]
        public string PhoneNumber { get; set; } = string.Empty;
        

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(300)]
        public string Address { get; set; } = string.Empty;

        // Navigation Property
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();


    }
}

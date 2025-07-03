using System.ComponentModel.DataAnnotations;

namespace Asset_Manager.Models  
{
    public class MaintenanceLog
    {
        [Key]
        public int  MaintenanceId { get; set; }

        [Required]
        public int AssetId { get; set; }
        public Asset Asset { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Description { get; set; } = string.Empty;

        [StringLength(100)]
        public string Tecnician { get; set; } = string.Empty;

        public DateTime LoggedDate { get; set; } = DateTime.Now;

        public DateTime? CompletionDate { get; set; }

        [StringLength(100)]
        public string Status { get; set; } = "Pending";//Pending, In Progress,Completed
    }
}

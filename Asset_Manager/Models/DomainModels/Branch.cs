using System.ComponentModel.DataAnnotations;

namespace Asset_Manager.Models  
{
    public class Branch
    {
        [Key]
        public int BranchId { get; set; }

        [Required(ErrorMessage = "Branch name is required.")]
        public string BranchName { get; set; } = string.Empty;

        public ICollection<Asset> Assets { get; set; } = new List<Asset>();
    }
}

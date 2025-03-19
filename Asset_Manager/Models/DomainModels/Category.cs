using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Asset_Manager.Models  
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required(ErrorMessage ="Category name cant be left blank.")]
        [StringLength(20)]
        public string CategoryName { get; set; } = string.Empty;

        public List<Asset> Assets { get; set; } = new List<Asset>();
    }
}

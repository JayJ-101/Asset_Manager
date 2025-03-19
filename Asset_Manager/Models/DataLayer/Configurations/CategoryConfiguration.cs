using Asset_Manager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Asset_Manager.Models
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> entity)
        {
            
            // seed initial data
            entity.HasData(
                new { CategoryId = 1, CategoryName = "Printer" },
                new { CategoryId = 2, CategoryName = "Desktop" }
            );
        }
    }
}

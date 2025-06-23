using Asset_Manager.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Asset_Manager.Models  
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> entity)
        {
            entity.HasOne(d => d.Branch)
                   .WithMany(b => b.Departments)
                   .HasForeignKey(d => d.BranchId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

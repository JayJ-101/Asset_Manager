using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asset_Manager.Models  
{
    public class AssignmentConfiguration : IEntityTypeConfiguration<AssetAssignment>
    {
        public void Configure(EntityTypeBuilder<AssetAssignment> entity)
        {
            entity.HasOne(a => a.Branch)
               .WithMany(b => b.AssetAssignments)
               .HasForeignKey(a => a.BranchId)
               .OnDelete(DeleteBehavior.Restrict);
            
            
            entity.HasOne(a => a.Department)
               .WithMany(d => d.AssetAssignments)
               .HasForeignKey(a => a.DepartmentId)
               .OnDelete(DeleteBehavior.Restrict);

            // One Asset can have multiple AssetAssignments
            entity.HasOne(aa => aa.Asset)
                   .WithMany(a => a.AssetAssignments)
                   .HasForeignKey(aa => aa.AssetId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

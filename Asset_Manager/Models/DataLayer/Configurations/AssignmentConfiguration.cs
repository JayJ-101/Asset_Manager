using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asset_Manager.Models  
{
    public class AssignmentConfiguration : IEntityTypeConfiguration<AssetAssignment>
    {
        public void Configure(EntityTypeBuilder<AssetAssignment> entity)
        {
            entity.HasOne(aa => aa.Branch)
            .WithMany()
            .HasForeignKey(a => a.BranchId);

            // One Asset can have multiple AssetAssignments
            entity.HasOne(aa => aa.Asset)
                   .WithMany(a => a.AssetAssignments)
                   .HasForeignKey(aa => aa.AssetId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

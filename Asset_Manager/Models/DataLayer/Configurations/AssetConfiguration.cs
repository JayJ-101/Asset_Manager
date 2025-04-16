using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Asset_Manager.Models  
{
    public class AssetConfiguration : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> entity)
        {
            entity.HasOne(a => a.Category)
                .WithMany(c => c.Assets)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);



            // Asset has one Supplier
            entity.HasOne(a => a.Supplier)
                  .WithMany(s => s.Assets)
                  .HasForeignKey(a => a.SupplierId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Asset has many Assignments
            entity.HasMany(a => a.AssetAssignments)
                  .WithOne(aa => aa.Asset)
                  .HasForeignKey(aa => aa.AssetId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Asset has many Maintenance Logs
            //entity.HasMany(a => a.MaintenanceLogs)
            //      .WithOne(m => m.Asset)
            //      .HasForeignKey(m => m.AssetId)
            //      .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

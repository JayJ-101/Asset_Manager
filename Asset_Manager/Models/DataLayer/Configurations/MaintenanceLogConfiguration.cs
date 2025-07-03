using Asset_Manager.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Asset_Manager.Models
{
    public class MaintenanceLogConfiguration : IEntityTypeConfiguration<MaintenanceLog>
    {
        public void Configure(EntityTypeBuilder<MaintenanceLog> builder)
        {
            builder.HasKey(m => m.MaintenanceId);

            builder.HasOne(m => m.Asset)
                .WithMany(a => a.MaintenanceLogs)
                .HasForeignKey(m => m.AssetId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asset_Manager.Models
{
    public class PurchaseOrderConfiguration : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> entity)
        {
            // Configure relationship with Supplier
            entity.HasOne(p => p.Supplier)
                  .WithMany(s => s.PurchaseOrders)
                  .HasForeignKey(p => p.SupplierId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Configure relationship with Asset
            entity.HasMany(p => p.Assets)
                  .WithOne(a => a.PurchaseOrder)
                  .HasForeignKey(a => a.PurchaseOrderId)
                  .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

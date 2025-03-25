using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Asset_Manager.Models
{
    public class PurchaseOrderItemConfig : IEntityTypeConfiguration<PurchaseOrderItem>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderItem> entity)
        {

            entity.HasOne(poi => poi.PurchaseOrder)
                .WithMany(po => po.Items)
                .HasForeignKey(poi => poi.PurchaseOrderId);

            entity.HasOne(poi => poi.Category)
                .WithMany()
                .HasForeignKey(poi => poi.CategoryId);
        }
    }
}

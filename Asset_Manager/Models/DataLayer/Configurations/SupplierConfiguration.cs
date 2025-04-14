using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Asset_Manager.Models
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> entity)
        {

            


            entity.HasData(
                new Supplier { SupplierId = 1, SupplierName = "Tech Supplier Co.", ContactPerson = "John Doe", PhoneNumber = "123456789", Email = "contact@techsupplier.com", Address = "123 Tech Street" }
            );
        }
    }

}

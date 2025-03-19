using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Asset_Manager.Models  
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> entity)
        {

            // seed initial data
            entity.HasData(
                new { BranchId = 1, BranchName = "Headquarters" }
               
            );
        }
    }
}

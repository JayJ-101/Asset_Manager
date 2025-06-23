using Asset_Manager.Models;
using Microsoft.EntityFrameworkCore;

namespace Asset_Manager.Models
{
    public class AssetDbContext :DbContext
    {
        public AssetDbContext(DbContextOptions<AssetDbContext> options) : base(options)
        {
        }
        public DbSet<Branch> Categories { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<Department> Departments { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AssetConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new BranchConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
           
            //modelBuilder.ApplyConfiguration(new AssignmentConfiguration());
            //modelBuilder.ApplyConfiguration(new MaintenanceLogConfiguration());
        }
    }
}

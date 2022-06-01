using DataCom.WebAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace DataCom.WebAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(builder =>
            {
                builder.HasKey(p => p.Id);
                builder.HasMany(p => p.Options)
                    .WithOne(o =>  o.Product);
                builder.Property(p => p.Price).HasColumnType("decimal(18, 2)");
                builder.Property(p => p.DeliveryPrice).HasColumnType("decimal(18, 2)");
            });

        }
    }
}

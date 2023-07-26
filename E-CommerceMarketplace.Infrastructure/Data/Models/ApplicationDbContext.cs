using E_CommerceMarketplace.Infrastructure.Data.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceMarketplace.Infrastructure.Data.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new StatusConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new VendorConfiguration());

            base.OnModelCreating(builder);
        }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Vendor> Vendors { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<Payment> Payments { get; set; }
    }
}
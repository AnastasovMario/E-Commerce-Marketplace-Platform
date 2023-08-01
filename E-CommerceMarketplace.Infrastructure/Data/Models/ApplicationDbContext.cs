using E_CommerceMarketplace.Infrastructure.Data.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace E_CommerceMarketplace.Infrastructure.Data.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
		public DbSet<Order> Orders { get; set; }

		public DbSet<Category> Categories { get; set; }

		public DbSet<Vendor> Vendors { get; set; }

		public DbSet<Product> Products { get; set; }

		public DbSet<Status> Statuses { get; set; }

		public DbSet<Sale> Sales { get; set; }

		public DbSet<ProductSale> ProductSales { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new StatusConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new VendorConfiguration());

            base.OnModelCreating(builder);

			builder.Entity<ProductSale>()
			.HasKey(ps => new { ps.ProductId, ps.SaleId });

			builder.Entity<ProductSale>()
				.HasOne(ps => ps.Product)
				.WithMany(p => p.ProductSales)
				.HasForeignKey(ps => ps.ProductId)
				.OnDelete(DeleteBehavior.Restrict);

			builder.Entity<ProductSale>()
				.HasOne(ps => ps.Sale)
				.WithMany(s => s.ProductSales)
				.HasForeignKey(ps => ps.SaleId)
				.OnDelete(DeleteBehavior.Restrict);
		}

        
	}
}
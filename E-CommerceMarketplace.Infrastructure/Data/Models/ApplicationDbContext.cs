﻿using E_CommerceMarketplace.Infrastructure.Data.Configuration;
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

		public DbSet<Item> Items { get; set; }

		public DbSet<Vendor> Vendors { get; set; }

		public DbSet<Product> Products { get; set; }

		public DbSet<Status> Statuses { get; set; }

		public DbSet<Sale> Sales { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration(new UserConfiguration());
			builder.ApplyConfiguration(new RoleConfiguration());
			builder.ApplyConfiguration(new UserRoleConfiguration());
			builder.ApplyConfiguration(new CategoryConfiguration());
			builder.ApplyConfiguration(new StatusConfiguration());
			builder.ApplyConfiguration(new ProductConfiguration());
			builder.ApplyConfiguration(new VendorConfiguration());

			base.OnModelCreating(builder);

			builder.Entity<Item>()
				.HasOne(item => item.Product)
				.WithMany()
				.HasForeignKey(item => item.Product_Id)
				.OnDelete(DeleteBehavior.NoAction);
		}
	}
}
using E_CommerceMarketplace.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceMarketplace.Infrastructure.Data.Configuration
{
	public class ProductConfiguration : IEntityTypeConfiguration<Product>
	{

		public void Configure(EntityTypeBuilder<Product> builder)
		{
			//builder.HasData(CreateProducts());
		}

		private List<Product> CreateProducts()
		{
			List<Product> products = new List<Product>()
	{
		new Product()
		{
			Id = 1,
			Name = "Smartphone XZ200",
			Price = 799.99m,
			ImageUrl = "https://www.pdevice.com/wp-content/uploads/2015/09/Gigaset-ME-pure-600x600.jpeg",
			Category_Id = Category.Electronics.Id,
			Vendor_Id = 1,
			Status_Id = Status.Stocked.Id,
        },

		new Product()
		{
			Id = 2,
			Name = "Men's Classic T-Shirt",
			Price = 19.99m,
			ImageUrl = "https://www.jottnar.com/cdn/shop/products/Productimage-Lodur-Turbulence-min_c13cd744-2711-4fa0-81d0-f312ed4a6a1a_3200x1800_crop_center.jpg?v=1681214994",
			Category_Id = Category.ClothingAndFashion.Id,
            Vendor_Id = 1,
			Status_Id = Status.Stocked.Id,
        },

		new Product()
		{
			Id = 3,
			Name = "Garden Tool Set",
			Price = 39.95m,
			ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR3B-zF-TdhbybX7l-51SJSrfSoDZLKEgxPuN-Och_y&s",
			Category_Id = Category.HomeAndGarden.Id,
            Vendor_Id = 1,
			Status_Id = Status.Stocked.Id,
        },

		new Product()
		{
			Id = 4,
			Name = "Luxury Watch",
			Price = 899.50m,
			ImageUrl = "https://cdn.anscommerce.com/catalog/brandstore/johnson/17_7_20/Sale.jpg",
			Category_Id = Category.Electronics.Id,
            Vendor_Id = 1,
			Status_Id = Status.Stocked.Id,
        },

		new Product()
		{
			Id = 5,
			Name = "Cookware Set",
			Price = 149.99m,
			ImageUrl = "https://m.media-amazon.com/images/I/61x-NhdKBmL.jpg",
			Category_Id = Category.HealthAndBeauty.Id,
            Vendor_Id = 1,
			Status_Id = Status.Stocked.Id,
		}
	};

			return products;
		}

	}
}

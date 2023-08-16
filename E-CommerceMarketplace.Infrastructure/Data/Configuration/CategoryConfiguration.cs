using E_CommerceMarketplace.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_CommerceMarketplace.Infrastructure.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(CreateCategories());
        }
        private List<Category> CreateCategories()
        {
            List<Category> categories = new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "Electronics"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Clothing & Fashion"
                },
                new Category()
                {
                    Id = 3,
                    Name = "Home & Garden"
                },
                new Category()
                {
                    Id = 4,
                    Name = "Health & Beauty"
                },
                new Category()
                {
                    Id = 5,
                    Name = "Books & Magazines"
                },
            };
            return categories;
        }

    }
}

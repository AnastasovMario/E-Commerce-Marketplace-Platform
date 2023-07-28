using E_CommerceMarketplace.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_CommerceMarketplace.Infrastructure.Data.Configuration
{
    internal class VendorConfiguration : IEntityTypeConfiguration<Vendor>
    {
        public void Configure(EntityTypeBuilder<Vendor> builder)
        {
            builder.HasData(new Vendor()
            {
                Id = 1,
                PhoneNumber = "+359888888888",
                User_Id = "dea12856-c198-4129-b3f3-b893d8395082"
            });
        }   
    }
}

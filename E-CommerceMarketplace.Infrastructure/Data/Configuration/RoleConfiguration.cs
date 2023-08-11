using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceMarketplace.Infrastructure.Data.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole { Id = "5f0b58c7-a226-4fc4-8177-a63ca6a23714", Name = "Administrator", NormalizedName = "ADMINISTRATOR", ConcurrencyStamp = "a579f4e5-08b5-412a-9724-f7f689120aa7" },
                new IdentityRole { Id = "809e5eef-aa05-4952-9caf-93bd802be499", Name = "User", NormalizedName = "USER", ConcurrencyStamp = "2b7499f6-211f-4315-97ec-4dee4402f35f" }
            );
        }
    }
}

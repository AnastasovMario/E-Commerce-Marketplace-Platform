using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceMarketplace.Infrastructure.Data.Configuration
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string> { UserId = "484e91df-4642-4a62-bd59-c448209c2def", RoleId = "5f0b58c7-a226-4fc4-8177-a63ca6a23714" },
                new IdentityUserRole<string> { UserId = "dea12856-c198-4129-b3f3-b893d8395082", RoleId = "809e5eef-aa05-4952-9caf-93bd802be499" },
                new IdentityUserRole<string> { UserId = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e", RoleId = "809e5eef-aa05-4952-9caf-93bd802be499" },
                new IdentityUserRole<string> { UserId = "6d4200ce-d726-4fc8-83d9-d6b3ac1f591e", RoleId = "809e5eef-aa05-4952-9caf-93bd802be499" }
            );
        }
    }
}

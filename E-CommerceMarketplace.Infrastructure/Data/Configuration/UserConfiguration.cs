﻿using E_CommerceMarketplace.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceMarketplace.Infrastructure.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
                .Property(p => p.IsActive)
                .HasDefaultValue(true);
            builder.HasData(CreateUsers());
        }

        private List<ApplicationUser> CreateUsers()
        {
            var users = new List<ApplicationUser>();
            var hasher = new PasswordHasher<ApplicationUser>();

            var user = new ApplicationUser()
            {
                Id = "484e91df-4642-4a62-bd59-c448209c2def",
                UserName = "admin@mail.com",
                NormalizedUserName = "admin@mail.com",
                Email = "admin@mail.com",
                NormalizedEmail = "admin@mail.com",
            };

            user.PasswordHash =
                 hasher.HashPassword(user, "admin123");

            users.Add(user);

            user = new ApplicationUser()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                UserName = "vendor@mail.com",
                NormalizedUserName = "vendor@mail.com",
                Email = "vendor@mail.com",
                NormalizedEmail = "vendor@mail.com",
            };

            user.PasswordHash =
                 hasher.HashPassword(user, "vendor");

            users.Add(user);

            user = new ApplicationUser()
            {
                Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                UserName = "guest@mail.com",
                NormalizedUserName = "guest@mail.com",
                Email = "guest@mail.com",
                NormalizedEmail = "guest@mail.com",
            };

            user.PasswordHash =
            hasher.HashPassword(user, "guest123");

            users.Add(user);

            user = new ApplicationUser()
            {
                Id = "6d4200ce-d726-4fc8-83d9-d6b3ac1f591e",
                UserName = "mario@mail.com",
                NormalizedUserName = "mario@mail.com",
                Email = "mario@mail.com",
                NormalizedEmail = "mario@mail.com",
            };

            user.PasswordHash =
            hasher.HashPassword(user, "mario123");

            users.Add(user);

            return users;
        }

    }
}

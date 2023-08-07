﻿using E_CommerceMarketplace.Infrastructure.DatabseConstants;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace E_CommerceMarketplace.Infrastructure
{
    public class ApplicationUser : IdentityUser
    {

        [MaxLength(DbConstants.UserFirstNameMaxLength)]
        public string? FirstName { get; init; }

        [MaxLength(DbConstants.UserLastNameMaxLength)]
        public string? LastName { get; init; }

        [Required]
        public bool IsActive { get; set; }
    }
}

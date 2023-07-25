using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_CommerceMarketplace.Infrastructure.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public const int UserFirstNameMaxLength = 12;
        public const int UserFirstNameMinLength = 1;

        public const int UserLastNameMaxLength = 12;
        public const int UserLastNameMinLength = 1;

        [Required]
        [MaxLength(UserFirstNameMaxLength)]
        public string FirstName { get; init; } = null!;

        [Required]
        [MaxLength(UserLastNameMaxLength)]
        public string LastName { get; init; } = null!;

        [Required]
        public bool IsActive { get; set; }
    }
}

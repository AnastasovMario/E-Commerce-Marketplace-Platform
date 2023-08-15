using E_CommerceMarketplace.Infrastructure.DatabseConstants;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static E_CommerceMarketplace.Infrastructure.DatabseConstants.DataConstants;

namespace E_CommerceMarketplace.Infrastructure.Data.Models
{
    public class ApplicationUser : IdentityUser
    {

        [MaxLength(UserFirstNameMaxLength)]
        public string? FirstName { get; set; }

        [MaxLength(UserLastNameMaxLength)]
        public string? LastName { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;
    }
}

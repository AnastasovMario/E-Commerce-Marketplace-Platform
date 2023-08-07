using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using E_CommerceMarketplace.Infrastructure.DatabseConstants;

namespace E_CommerceMarketplace.Infrastructure.Data.Models
{
    public class Vendor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Phone]
        [StringLength(15)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
		[StringLength(DbConstants.UserFirstNameMaxLength, MinimumLength = DbConstants.UserFirstNameMinLength)]
		public string FirstName { get; init; } = null!;

        [Required]
		[StringLength(DbConstants.UserLastNameMaxLength, MinimumLength =DbConstants.UserLastNameMinLength)]
		public string LastName { get; init; } = null!;

		[Required]
		[ForeignKey(nameof(User))]
		public string User_Id { get; set; } = null!;
        
        public ApplicationUser User { get; set; } = null!;
    }
}
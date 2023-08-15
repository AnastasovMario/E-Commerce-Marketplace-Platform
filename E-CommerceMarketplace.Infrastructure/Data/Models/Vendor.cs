using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using E_CommerceMarketplace.Infrastructure.DatabseConstants;
using static E_CommerceMarketplace.Infrastructure.DatabseConstants.DataConstants;

namespace E_CommerceMarketplace.Infrastructure.Data.Models
{
    public class Vendor
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Phone]
        [StringLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; } = null!;

        [Required]
		[StringLength(UserFirstNameMaxLength)]
		public string FirstName { get; init; } = null!;

        [Required]
		[StringLength(UserLastNameMaxLength)]
		public string LastName { get; init; } = null!;

		[Required]
		[ForeignKey(nameof(User))]
		public string User_Id { get; set; } = null!;
        
        public ApplicationUser User { get; set; } = null!;
    }
}
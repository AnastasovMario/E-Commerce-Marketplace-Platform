using System.ComponentModel.DataAnnotations;
using static E_CommerceMarketplace.Infrastructure.DatabseConstants.DataConstants;

namespace E_CommerceMarketplace.Core.Models.Vendor
{
    public class BecomeVendorModel
	{

		[Required]
		[StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
		[Phone]
		[Display(Name = "Phone number")]
		public string PhoneNumber { get; set; } = null!;

		[Required]
		[Display(Name = "First name")]
		[StringLength(UserFirstNameMaxLength, MinimumLength = UserFirstNameMinLength)]
		public string FirstName { get; set; } = null!;

		[Required]
        [Display(Name = "Last name")]
        [StringLength(UserLastNameMaxLength, MinimumLength = UserLastNameMinLength)]
		public string LastName { get; set; } = null!;
	}
}

using E_CommerceMarketplace.Infrastructure.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using E_CommerceMarketplace.Infrastructure.DatabseConstants;

namespace E_CommerceMarketplace.Core.Models.Vendor
{

	public class BecomeVendorModel
	{

		[Required]
		[StringLength(DbConstants.PhoneNumberMaxLength, MinimumLength = DbConstants.PhoneNumberMinLength)]
		[Phone]
		[Display(Name = "Phone number")]
		public string PhoneNumber { get; set; } = null!;

		[Required]
		[Display(Name = "First name")]
		[StringLength(DbConstants.UserFirstNameMaxLength, MinimumLength = DbConstants.UserFirstNameMinLength)]
		public string FirstName { get; set; } = null!;

		[Required]
        [Display(Name = "Last name")]
        [StringLength(DbConstants.UserLastNameMaxLength, MinimumLength = DbConstants.UserLastNameMinLength)]
		public string LastName { get; set; } = null!;
	}
}

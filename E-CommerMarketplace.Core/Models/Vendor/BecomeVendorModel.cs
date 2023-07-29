using E_CommerceMarketplace.Infrastructure.Data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using E_CommerceMarketplace.Infrastructure.Constants;

namespace E_CommerceMarketplace.Core.Models.Vendor
{

	public class BecomeVendorModel
	{
		public const int UserFirstNameMaxLength = 20;
		public const int UserFirstNameMinLength = 1;

		public const int UserLastNameMaxLength = 20;
		public const int UserLastNameMinLength = 1;

		[Required]
		[StringLength(15, MinimumLength = 7)]
		[Phone]
		[Display(Name = "Phone number")]
		public string PhoneNumber { get; set; } = null!;

		[Required]
		[StringLength(UserFirstNameMaxLength, MinimumLength = UserFirstNameMinLength)]
		public string FirstName { get; init; } = null!;

		[Required]
		[StringLength(UserLastNameMaxLength, MinimumLength = UserLastNameMinLength)]
		public string LastName { get; init; } = null!;
	}
}

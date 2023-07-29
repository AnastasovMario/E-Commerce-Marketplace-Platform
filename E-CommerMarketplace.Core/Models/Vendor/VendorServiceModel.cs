using System.ComponentModel.DataAnnotations;

namespace E_CommerceMarketplace.Core.Models.Vendor
{
    public class VendorServiceModel
    {
        public string PhoneNumber { get; set; } = null!;

        public string FirstName { get; init; } = null!;

        public string LastName { get; init; } = null!;
    }
}

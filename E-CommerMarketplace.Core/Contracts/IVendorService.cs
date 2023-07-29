using E_CommerceMarketplace.Core.Models.Vendor;

namespace E_CommerceMarketplace.Core.Contracts
{
	public interface IVendorService
	{
		Task Create(string userId, BecomeVendorModel model);

		Task<bool> ExistsById(string userId);

		Task<bool> UserWithPhoneNumberExists(string phoneNumber);

		Task<int> GetVendorId(string userId);
	}
}

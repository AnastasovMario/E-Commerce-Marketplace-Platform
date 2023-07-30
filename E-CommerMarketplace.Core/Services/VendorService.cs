using E_CommerceMarketplace.Core.Contracts;
using E_CommerceMarketplace.Core.Models.Vendor;
using E_CommerceMarketplace.Infrastructure.Common;
using E_CommerceMarketplace.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceMarketplace.Core.Services
{
	public class VendorService : IVendorService
	{
		private readonly IRepository repository;
		public VendorService(IRepository _repository)
		{
			repository = _repository;
		}

		public async Task Create(string userId, BecomeVendorModel model)
		{
			var user = await repository.GetByIdAsync<ApplicationUser>(userId);
			if (user == null)
			{
				throw new ArgumentException("User was not found");
			}
			var vendor = new Vendor()
			{
				FirstName = model.FirstName,
				LastName = model.LastName,
				PhoneNumber = model.PhoneNumber,
				User_Id = user.Id,
			};

			await repository.AddAsync(vendor);
			await repository.SaveChangesAsync();
		}

		public async Task<bool> ExistsById(string userId)
		{
			return await repository.AllReadonly<Vendor>()
				.AnyAsync(u => u.User_Id == userId && u.User.IsActive == true);
		}

		public async Task<int> GetVendorId(string userId)
		{
			return await repository.AllReadonly<Vendor>()
				.Where(v => v.User_Id == userId)
				.Select(v => v.Id)
				.FirstOrDefaultAsync();
		}

		public async Task<bool> UserWithPhoneNumberExists(string phoneNumber)
		{
			return await repository.All<Vendor>()
				.AnyAsync(a => a.PhoneNumber == phoneNumber && a.User.IsActive == true);
		}
	}
}

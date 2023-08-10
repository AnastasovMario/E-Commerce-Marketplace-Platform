using E_CommerceMarketplace.Core.Contracts.Admin;
using E_CommerceMarketplace.Core.Models.Admin;
using E_CommerceMarketplace.Infrastructure.Common;
using E_CommerceMarketplace.Infrastructure.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_CommerceMarketplace.Core.Services.Admin
{
    public class UserService : IUserService
    {
        private readonly IRepository repo;

        private readonly UserManager<ApplicationUser> userManager;

        public UserService(
            IRepository _repo,
            UserManager<ApplicationUser> _userManager)
        {
            repo = _repo;
            userManager = _userManager;
        }

        public async Task<IEnumerable<UserServiceModel>> All()
        {
            List<UserServiceModel> result;

            result = await repo.AllReadonly<Vendor>()
                .Where(v => v.User.IsActive)
                .Select(v => new UserServiceModel()
                {
                    UserId = v.User_Id,
                    Email = v.User.Email,
                    FullName = $"{v.FirstName} {v.LastName}",
                    PhoneNumber = v.PhoneNumber
                })
                .ToListAsync();

            string[] vendorIds = result.Select(a => a.UserId).ToArray();

            result.AddRange(await repo.AllReadonly<ApplicationUser>()
                .Where(u => vendorIds.Contains(u.Id) == false)
                .Where(u => u.IsActive)
                .Select(u => new UserServiceModel()
                {
                    UserId = u.Id,
                    Email = u.Email,
                    FullName = $"{u.FirstName} {u.LastName}"
                }).ToListAsync());

            return result;
        }

        public async Task<bool> Forget(string userId)
        {
            var user = await userManager.FindByIdAsync(userId);

            user.PhoneNumber = null;
            user.Email = null;
            user.IsActive = false;
            user.NormalizedEmail = null;
            user.NormalizedUserName = null;
            user.PasswordHash = null;
            user.UserName = $"forgottenUser-{DateTime.Now.Ticks}";

            var result = await userManager.UpdateAsync(user);

            return result.Succeeded;
        }

        public async Task<string> UserFullName(string userId)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(userId);

            return $"{user?.FirstName} {user?.LastName}".Trim();
        }
    }
}

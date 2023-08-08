using E_CommerceMarketplace.Core.Models.Admin;

namespace E_CommerceMarketplace.Core.Contracts.Admin
{
    public interface IUserService
    {
        Task<string> UserFullName(string userId);

        Task<IEnumerable<UserServiceModel>> All();

        Task<bool> Forget(string userId);
    }
}

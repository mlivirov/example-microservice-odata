using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProjectName.Sso.Application.Database;

namespace ProjectName.Sso.Application.Services
{
    public interface IApplicationUserStore : IUserStore<User>
    {
        Task<bool> ValidateCredentials(string username, string password);

        IEnumerable<Claim> GetClaims(User user);

        Task<User> FindByExternalProviderAsync(string provider, string providerUserId);
    }
}
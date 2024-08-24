using VirtualShop.Application.Common.Models;

namespace VirtualShop.Application.Common.Interfaces;
public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(ApplicationUserCreateModel userData);

    Task<Result> DeleteUserAsync(string userId);
}


using VirtualShop.Application.Common.Models;
using VirtualShop.Application.ShopUser.Commands.RegisterUser;

namespace VirtualShop.Application.Common.Interfaces;
public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName); 
    Task<Result> Login(string username, string password);
    Task<(Result Result, string UserId)> CreateUserAsync(string email, string password, string userName, string firstName, string lastName, string address, string postalCode);

    Task<Result> DeleteUserAsync(string userId);
    Task<bool> isUniqueUsername(string userName);
    Task<bool> isUniqueEmail(string email);
}


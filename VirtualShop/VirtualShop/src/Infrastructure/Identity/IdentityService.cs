using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using VirtualShop.Application.Common.Interfaces;
using VirtualShop.Application.Common.Models;
using VirtualShop.Application.Customer.Commands.RegisterUser;
using VirtualShop.Domain.Constants;

namespace VirtualShop.Infrastructure.Identity;
public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user?.UserName;
    }

    public async Task<(Result Result, string UserId)> CreateUserAsync(string email, string password, string userName)
    {
        var user = new ApplicationUser
        {
            UserName = userName,
            Email = email
        };

        var result = await _userManager.CreateAsync(user, password);

        return (result.ToApplicationResult(), user.Id);
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Result> DeleteUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user != null ? await DeleteUserAsync(user) : Result.Success();
    }

    public async Task<Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

    public async Task<bool> isUniqueUsername(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        return user == null;
    }

    public async Task<bool> isUniqueEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        return user == null;
    }

    public async Task<Result> Login(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);
        if (user is null)
        {
            return Result.Failure(["No such user"]);
        }
        if (user.IsActive == false)
        {
            return Result.Failure(["this user is not active"]);
        }

        var useCookieScheme = false;
        var isPersistent = false;
        _signInManager.AuthenticationScheme = useCookieScheme ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;

        var result = await _signInManager.PasswordSignInAsync(username, password, isPersistent, lockoutOnFailure: true);

        if (!result.Succeeded)
        {
            return Result.Failure([result.ToString()]);
        }
        return Result.Success();
    }

    public async Task<Result> DeactivateUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            return Result.Failure(["No such user"]);
        }
        user.IsActive = false;
        var result = await _userManager.UpdateAsync(user);
        return result.ToApplicationResult();

    }

    public async Task<Result> AddToRoleAsync(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null)
        {
            return Result.Failure(["No such user"]);
        }

        var role = new IdentityRole(roleName);

        if (_roleManager.Roles.All(r => r.Name != role.Name))
        {
            var result = await _roleManager.CreateAsync(role);
            if (result.Succeeded == false)
            {
                return result.ToApplicationResult();
            }
        }
        if (!string.IsNullOrWhiteSpace(role.Name))
        {
            var result = await _userManager.AddToRolesAsync(user, new[] { role.Name });
            if (result.Succeeded == false)
            {
                return result.ToApplicationResult();
            }
        }
        return Result.Success();
    }
}

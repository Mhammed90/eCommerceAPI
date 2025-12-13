using eCommerce.Domain.Entities.Identity;
using eCommerce.Domain.Interfaces.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;

namespace eCommerce.Infrastructure.Repositories.Authentication;

public class RoleManagement : IRoleManagement
{
    private readonly UserManager<AppUser> _userManager;

    public RoleManagement(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string?> GetUserRoleAsync(string userEmail)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        return (await _userManager.GetRolesAsync(user)).FirstOrDefault();
    }

    public async Task<bool> AddUserToRoleAsync(AppUser user, string role)
    {
        var result = await _userManager.AddToRoleAsync(user, role);
        return result.Succeeded;
    }
}
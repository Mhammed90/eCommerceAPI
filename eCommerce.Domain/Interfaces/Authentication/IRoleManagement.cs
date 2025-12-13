using eCommerce.Domain.Entities.Identity;

namespace eCommerce.Domain.Interfaces.Authentication;

public interface IRoleManagement
{
    Task<string?> GetUserRoleAsync(string userEmail);
    Task<bool> AddUserToRoleAsync(AppUser user, string role);
}
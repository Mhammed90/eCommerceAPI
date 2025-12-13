using System.Security.Claims;
using eCommerce.Domain.Entities.Identity;

namespace eCommerce.Domain.Interfaces.Authentication;

public interface IUserManagement
{
    Task<bool> CreateUserAsync(AppUser user);
    Task<bool> LoginUserAsync(AppUser user);
    Task<AppUser?> GetUserByEmailAsync(string userEmail);
    Task<AppUser?> GetUserByIdAsync(string userId);
    Task<IEnumerable<AppUser>?> GetAllUsersAsync();
    Task<int> RemoveUserByEmailAsync(string userEmail);
    Task<List<Claim>> GetUserClaimsAsync(string userEmail);
}
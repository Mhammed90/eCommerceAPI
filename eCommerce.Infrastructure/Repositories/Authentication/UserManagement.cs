using System.Security.Claims;
using eCommerce.Domain.Entities.Identity;
using eCommerce.Domain.Interfaces.Authentication;
using eCommerce.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Repositories.Authentication;

public class UserManagement : IUserManagement

{
    private readonly UserManager<AppUser> _userManager;
    private readonly IRoleManagement _roleManagement;
    private readonly AppDbContext _context;

    public UserManagement(UserManager<AppUser> userManager, IRoleManagement roleManagement, AppDbContext Context)
    {
        _userManager = userManager;
        _roleManagement = roleManagement;
        _context = Context;
    }

    public async Task<bool> CreateUserAsync(AppUser user)
    {
        var userByEmailAsync = await GetUserByEmailAsync (user.Email!);
        if (userByEmailAsync != null) return false;
        return (await _userManager.CreateAsync(user, user.PasswordHash!)).Succeeded;
    }


    public async Task<AppUser?> GetUserByEmailAsync(string userEmail)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        return user;
    }

    public async Task<AppUser?> GetUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        return user;
    }

    public async Task<IEnumerable<AppUser>?> GetAllUsersAsync()
    {
        var result = await _context.Users.ToListAsync();
        return result;
    }


    public async Task<List<Claim>> GetUserClaimsAsync(string userEmail)
    {
        var user = await GetUserByEmailAsync(userEmail);
        string userRoleName = await _roleManagement.GetUserRoleAsync(user.Email!);
        List<Claim> claims = new List<Claim>();
        claims.Add(new Claim(ClaimTypes.Role, userRoleName!));
        claims.Add(new Claim(ClaimTypes.Email, user.Email!));
        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
        claims.Add(new Claim("FullName", user.FullName!));
        return claims;
    }

    public async Task<int> RemoveUserByEmailAsync(string userEmail)
    {
        var user = await _context.Users.FirstOrDefaultAsync(email => userEmail == email.Email);
        if (user != null) _context.Users.Remove(user);
        return await _context.SaveChangesAsync();
    }

    public async Task<bool> LoginUserAsync(AppUser user)
    {
        var userByEmailAsync = await GetUserByEmailAsync(user.Email!);
        if (userByEmailAsync == null) return false;
        var roles = await _roleManagement.GetUserRoleAsync(user.Email!);
        if (roles == null) return false;
        var userExists = await _userManager.CheckPasswordAsync(userByEmailAsync, user.PasswordHash!);
        return userExists;
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using eCommerce.Domain.Entities.Identity;
using eCommerce.Domain.Interfaces.Authentication;
using eCommerce.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace eCommerce.Infrastructure.Repositories.Authentication;

public class TokenManagement : ITokenManagement
{
    private readonly UserManager<AppUser> _userManager;
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly IUserManagement _userManagement;

    public TokenManagement(UserManager<AppUser> userManager, AppDbContext context, IConfiguration configuration,
        IUserManagement userManagement)
    {
        _userManager = userManager;
        _context = context;
        _configuration = configuration;
        _userManagement = userManagement;
    }

    public string GetRefreshToken()
    {
        byte[] randomBytes = new byte[64];
        RandomNumberGenerator.Fill(randomBytes);
        string token = Convert.ToBase64String(randomBytes)
            .Replace("+", "-")
            .Replace("/", "_")
            .TrimEnd('=');
        return token;
    }

    public List<Claim> GetUserClaimsFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

        if (jwtToken == null)
            return [];
        return jwtToken.Claims.ToList();
    }

    public async Task<bool> ValidateRefreshTokenAsync(string refreshToken)
    {
        var user = await _context.RefreshTokens.FirstOrDefaultAsync(refToken => refToken.Token == refreshToken);
        return user != null;
    }

    public async Task<string?> GetUserIdByRefreshToken(string refreshToken)
    {
        var result = await _context.RefreshTokens.FirstOrDefaultAsync(refToken => refreshToken == refToken.Token);
        return result!.UserId;
    }

    public Task<int> AddRefreshToken(string useId, string refreshToken)
    {
        _context.RefreshTokens.Add(new RefreshToken { UserId = useId, Token = refreshToken });
        return _context.SaveChangesAsync();
    }

    public async Task<int> UpdateRefreshToken(string userId, string refreshToken)
    {
        var user = await _context.RefreshTokens.FirstOrDefaultAsync(id => id.UserId == userId);
        if (user == null)
            return -1;
        user.Token = refreshToken;
        return await _context.SaveChangesAsync();
    }

    public string GenerateToken(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddHours(5);
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            expires: expires,
            signingCredentials: creds,
            claims: claims
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
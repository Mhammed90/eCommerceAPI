using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Identity;

namespace eCommerce.Application.Services.Interfaces.Authentication;

public interface IAuthentciationService
{
    Task<ServiceResponse> CreateUser(CreateUser user);
    Task<LoginResponse> LoginUser(LoginUser user);
    Task<LoginResponse> ReviveToken(string refreshToken);
}
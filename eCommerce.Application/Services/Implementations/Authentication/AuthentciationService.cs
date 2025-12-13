using AutoMapper;
using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Identity;
using eCommerce.Application.Services.Interfaces.Authentication;
using eCommerce.Application.Services.Logging;
using eCommerce.Application.Validation;
using eCommerce.Domain.Entities.Identity;
using eCommerce.Domain.Interfaces.Authentication;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;

namespace eCommerce.Application.Services.Implementations.Authentication;

public class AuthentciationService : IAuthentciationService
{
    private readonly ITokenManagement _tokenManagement;
    private readonly IUserManagement _userManagement;
    private readonly IRoleManagement _roleManagement;
    private readonly IAppLogger<AuthenticationService> _logger;
    private readonly IMapper _mapper;
    private readonly IValidator<LoginUser> _loginUserValidator;
    private readonly IValidator<CreateUser> _createUserValidator;
    private readonly IValidationService _validationService;


    public AuthentciationService(ITokenManagement tokenManagement, IRoleManagement roleManagement,
        IUserManagement userManagement, IAppLogger<AuthenticationService> logger, IMapper mapper,
        IValidator<LoginUser> loginUserValidator, IValidator<CreateUser> createUserValidator,
        IValidationService validationService)
    {
        _tokenManagement = tokenManagement;
        _roleManagement = roleManagement;
        _userManagement = userManagement;
        _logger = logger;
        _mapper = mapper;
        _loginUserValidator = loginUserValidator;
        _createUserValidator = createUserValidator;
        _validationService = validationService;
    }

    public async Task<ServiceResponse> CreateUser(CreateUser user)
    {
        var valdation = await _validationService.ValidateUser(user, _createUserValidator);
        if (!valdation.Success)
        {
            return valdation;
        }

        var mappedUser = _mapper.Map<AppUser>(user);
        mappedUser.UserName = user.Email;
        mappedUser.PasswordHash = user.Password;
        var result = await _userManagement.CreateUserAsync(mappedUser);
        if (!result)
        {
            return new ServiceResponse
            {
                Success = false, Message = "Email Address might be exist or unknown error occured."
            };
        }

        var userByEmail = await _userManagement.GetUserByEmailAsync(user.Email);
        var usersCont = (await _userManagement.GetAllUsersAsync()).Count();
        bool assignRole = await _roleManagement.AddUserToRoleAsync(userByEmail!, usersCont > 1 ? "User" : "Admin");
        if (!assignRole)
        {
            int removeUser = await _userManagement.RemoveUserByEmailAsync(userByEmail!.Email!);
            if (removeUser <= 0)
            {
                _logger.LogInformation($"User {user.Email} failed to be removed.");
                return new ServiceResponse
                {
                    Success = false,
                    Message = "Error Occured while creating account."
                };
            }
        }

        // verify email 


        return new ServiceResponse
        {
            Success = true, Message = "User Created Successfully"
        };
    }

    public async Task<LoginResponse> LoginUser(LoginUser user)
    {
        var validation = await _validationService.ValidateUser(user, _loginUserValidator);
        if (validation.Success == false)
        {
            return new LoginResponse { Message = validation.Message, Success = false };
        }

        var mappedUser = _mapper.Map<AppUser>(user);
        mappedUser.PasswordHash = user.Password;
        var result = await _userManagement.LoginUserAsync(mappedUser);
        if (!result)
        {
            return new LoginResponse { Message = "Email not found or incorrect password." };
        }

        var userByEmail = await _userManagement.GetUserByEmailAsync(user.Email);
        var claims = await _userManagement.GetUserClaimsAsync(userByEmail!.Email);
        string jwtToken = _tokenManagement.GenerateToken(claims);
        string refreshToken = _tokenManagement.GetRefreshToken();

        int saveTokenResult = await _tokenManagement.AddRefreshToken(userByEmail.Id, refreshToken);
        if (saveTokenResult <= 0)
            return new LoginResponse
            {
                Message = "Internal Server Error. Please try again later."
            };

        return new LoginResponse { Success = true, Token = jwtToken, RefreshToken = refreshToken };
    }


    public async Task<LoginResponse> ReviveToken(string refreshToken)
    {
        bool validateToken = await _tokenManagement.ValidateRefreshTokenAsync(refreshToken);
        if (!validateToken)
            return new LoginResponse { Message = "Refresh Token is invalid." };
        var userId = await _tokenManagement.GetUserIdByRefreshToken(refreshToken);
        var user = await _userManagement.GetUserByIdAsync(userId!);
        var claims = await _userManagement.GetUserClaimsAsync(user!.Email!);
        var newJwtToken = _tokenManagement.GenerateToken(claims);
        var newRefreshToken = _tokenManagement.GetRefreshToken();
        await _tokenManagement.UpdateRefreshToken(userId!, newRefreshToken);

        return new LoginResponse { Success = true, Token = newJwtToken, RefreshToken = newRefreshToken };
    }
}
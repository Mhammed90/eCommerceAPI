using eCommerce.Application.DTOs.Identity;
using eCommerce.Application.Services.Interfaces.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Presentation.Controller;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthentciationService _authentciationService;

    public AuthenticationController(IAuthentciationService authentciationService)
    {
        _authentciationService = authentciationService;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateUser(CreateUser createUser)
    {
        var result = await _authentciationService.CreateUser(createUser);
        if (!result.Success)
            return BadRequest(result);
        return Ok(result);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginUser loginUser)
    {
        var result = await _authentciationService.LoginUser(loginUser);
        if (!result.Success)
            return BadRequest(result);
        return Ok(result);
    }

    [HttpGet("refreshToken/{refreshToken}")]
    public async Task<IActionResult> RefreshToken(string refreshToken)
    {
        var result = await _authentciationService.ReviveToken(refreshToken);
        if (!result.Success)
            return BadRequest(result);
        return Ok(result);
    }
}
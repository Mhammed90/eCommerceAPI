using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.Services.Interfaces.Cart;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Presentation.Controller;

[Route("api/[controller]")]
[ApiController]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }

    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout(Checkout checkout)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var result = await _cartService.Checkout(checkout);
        if (result.Success)
            return Ok(result);
        return BadRequest(result);
    }
 
    [HttpGet("Save-Checkouts")]
    public async Task<IActionResult> SaveCheckout(IEnumerable<CreateAchieve> createAchieve)

    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _cartService.SaveCheckoutHistory(createAchieve);
        if (result.Success)
            return Ok(result); 
        
        return BadRequest(result);
    }
}
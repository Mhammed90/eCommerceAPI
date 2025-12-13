using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.Services.Interfaces.Cart;
using eCommerce.Domain.Entities.Cart;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Presentation.Controller;

[Route("api/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentMethodService _paymentMethodService;

    public PaymentController(IPaymentMethodService paymentMethodService)
    {
        _paymentMethodService = paymentMethodService;
    }

    [HttpGet("PaymentMethods")]
    public async Task<ActionResult<IEnumerable<GetPaymentMethod>>> GetPaymentMethods()
    {
        var result = await _paymentMethodService.GetPaymentMethodAsync();

        if (!result.Any())
            return NotFound();
        return Ok(result);
    }
}
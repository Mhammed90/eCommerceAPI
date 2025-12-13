using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Cart;
using eCommerce.Domain.Entities.Cart;

namespace eCommerce.Application.Services.Interfaces.Cart;

public interface ICartService
{
    Task<ServiceResponse> SaveCheckoutHistory(IEnumerable<CreateAchieve> checkouts);
    Task<ServiceResponse> Checkout(Checkout checkout);
}
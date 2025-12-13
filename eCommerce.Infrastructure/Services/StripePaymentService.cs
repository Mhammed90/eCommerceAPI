using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.Services.Interfaces.Cart;
using eCommerce.Domain.Entities;
using Stripe.Checkout;

namespace eCommerce.Infrastructure.Services;

public class StripePaymentService : IPaymentService
{
    public async Task<ServiceResponse> Pay(decimal totalAmount, IEnumerable<Product> cartProducts,
        IEnumerable<ProcessCart> carts)
    {
        try
        {
            var lineItems = new List<SessionLineItemOptions>();
            foreach (var item in cartProducts)
            {
                var pQuantity = carts.FirstOrDefault(p => p.ProductId == item.Id);
                lineItems.Add(new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "ust",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = item.Name, Description = item.Description
                        },
                        UnitAmount = (long)(item.Price * 100)
                    },
                    Quantity = pQuantity!.Quantity
                });
            }

            var options = new SessionCreateOptions
            {
                LineItems = lineItems,
                PaymentMethodTypes = ["usd"],
                Mode = "Payment",
                SuccessUrl = "https://localhost:5000/success",
                CancelUrl = "https://localhost:5000/cancel"

            };
            var service = new SessionService();
            Session session = await service.CreateAsync(options);
            return new ServiceResponse(true, session.Url);
        }
        catch (Exception ex)
        {
            return new ServiceResponse(false, ex.Message);
        }
    }
}
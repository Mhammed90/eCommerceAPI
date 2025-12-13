using AutoMapper;
using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.Services.Interfaces;
using eCommerce.Application.Services.Interfaces.Cart;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Entities.Cart;
using eCommerce.Domain.Interfaces;
using eCommerce.Domain.Interfaces.Cart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace eCommerce.Application.Services.Implementations.Cart;

public class CartService : ICartService
{
    private readonly ICart _cart;
    private readonly IMapper _mapper;
    // private readonly IGeneric<Product> _product;

    private readonly IProductService _productService;
    private readonly IPaymentMethodService _paymentMethodService;
    private readonly IPaymentService _paymentService;

    public CartService(ICart cart, IMapper mapper, IProductService productService,
        IPaymentMethodService paymentMethodService, IPaymentService paymentService)
    {
        _cart = cart;
        _mapper = mapper;
        _productService = productService;
        _paymentMethodService = paymentMethodService;
        _paymentService = paymentService;
    }

    public async Task<ServiceResponse> SaveCheckoutHistory(IEnumerable<CreateAchieve> checkouts)
    {
        var mappedCheckouts = _mapper.Map<IEnumerable<Achieve>>(checkouts);
        var result = await _cart.SaveCheckoutHistory(mappedCheckouts);
        if (result <= 0)
            return new ServiceResponse(Success: false, Message: "Error saving checkout history");

        return new ServiceResponse(Success: true, Message: "Checkout history saved");
    }

    public async Task<ServiceResponse> Checkout(Checkout checkout )
    {
        var (products, totalAmount) = await GetTotalAmount(checkout.Carts);
        var paymentMethods = await _paymentMethodService.GetPaymentMethodAsync();

        if (paymentMethods.FirstOrDefault()!.Id == checkout.PaymentMethodId)
            return await _paymentService.Pay(totalAmount, products, checkout.Carts);

        return new ServiceResponse(Success: false, Message: "Checkout failed");
    }

    private async Task<(IEnumerable<Product>, decimal)> GetTotalAmount(IEnumerable<ProcessCart> carts)
    {
        if (!carts.Any())
            return ([], 0);
        var products = await _productService.GetAllAsync();
        if (!products.Any())
            return ([], 0);
        var cartProducts = carts.Select(cartItem => products.FirstOrDefault(p => p.Id == cartItem.ProductId))
            .Where(product => product != null).ToList();
        var totalAmount = carts.Where(cartItem => cartProducts.Any(p => p.Id == cartItem.ProductId)).Sum(cartItem =>
            cartItem.Quantity * cartProducts.First(p => p.Id == cartItem.ProductId).Price);
        return ((IEnumerable<Product>, decimal))(cartProducts, totalAmount);
    }
}
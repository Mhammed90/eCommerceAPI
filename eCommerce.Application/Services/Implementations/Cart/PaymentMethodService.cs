using AutoMapper;
using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.Services.Interfaces.Cart;
using eCommerce.Domain.Interfaces.Cart;

namespace eCommerce.Application.Services.Implementations.Cart;

public class PaymentMethodService : IPaymentMethodService
{
    private readonly IPaymentMethod _paymentMethod;
    private readonly IMapper _mapper;

    public PaymentMethodService(IPaymentMethod paymentMethod, IMapper mapper)
    {
        _paymentMethod = paymentMethod;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GetPaymentMethod>> GetPaymentMethodAsync()
    {
        var methods = await _paymentMethod.GetPaymentMethodAsync();

        if (!methods.Any())
            return [];
        var mapped = _mapper.Map<IEnumerable<GetPaymentMethod>>(_paymentMethod);
        return mapped;
    }
}
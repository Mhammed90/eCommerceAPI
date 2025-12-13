using eCommerce.Domain.Entities.Cart;
using eCommerce.Domain.Interfaces.Cart;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Repositories.Cart;

public class PaymentMethodRepository : IPaymentMethod
{
    private readonly AppDbContext _dbContext;

    public PaymentMethodRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<PaymentMethod>> GetPaymentMethodAsync()
    {
        return await _dbContext.PaymentMethods.AsNoTracking().ToListAsync();
    }
}
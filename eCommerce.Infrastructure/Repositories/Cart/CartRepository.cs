using eCommerce.Domain.Entities.Cart;
using eCommerce.Domain.Interfaces.Cart;
using eCommerce.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Repositories.Cart;

public class CartRepository : ICart
{
    private readonly AppDbContext _dbContext;

    public CartRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<int> SaveCheckoutHistory(IEnumerable<Achieve> checkouts)

    {
        _dbContext.CheckoutAchieves.AddRange(checkouts);
        return await _dbContext.SaveChangesAsync();
    }
}
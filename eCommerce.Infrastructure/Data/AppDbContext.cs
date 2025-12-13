using eCommerce.Domain.Entities;
using eCommerce.Domain.Entities.Cart;
using eCommerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Infrastructure.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<PaymentMethod> PaymentMethods { get; set; }
    public DbSet<Achieve> CheckoutAchieves { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "User", NormalizedName = "USER" }
        );
        builder.Entity<PaymentMethod>().HasData(
            new PaymentMethod { Id = Guid.NewGuid(), Name = "Credit/Debit Card" },
            new PaymentMethod { Id = Guid.NewGuid(), Name = "PayPal" },
            new PaymentMethod { Id = Guid.NewGuid(), Name = "Cash on Delivery" },
            new PaymentMethod { Id = Guid.NewGuid(), Name = "Bank Transfer" },
            new PaymentMethod { Id = Guid.NewGuid(), Name = "Vodafone Cash" },
            new PaymentMethod { Id = Guid.NewGuid(), Name = "Fawry" },
            new PaymentMethod { Id = Guid.NewGuid(), Name = "Apple Pay" },
            new PaymentMethod { Id = Guid.NewGuid(), Name = "Google Pay" }
        );
    }
}
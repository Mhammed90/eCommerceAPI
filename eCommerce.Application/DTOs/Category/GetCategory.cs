using eCommerce.Application.DTOs.Product;
using eCommerce.Application.Services.Interfaces;

namespace eCommerce.Application.DTOs.Category;

public class GetCategory : CategoryBase
{
    public Guid Id { get; set; }
    public ICollection<GetProduct>? Products { get; set; }
}
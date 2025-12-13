using System.ComponentModel.DataAnnotations;
using eCommerce.Application.DTOs.Category;

namespace eCommerce.Application.DTOs.Product;

public class GetProduct : ProductBase
{
     [Required]
    public Guid Id { get; set; }
    public GetCategory? Category { get; set; }
}
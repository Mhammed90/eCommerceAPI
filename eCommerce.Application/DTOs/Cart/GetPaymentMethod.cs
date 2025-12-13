using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.Cart;

public class GetPaymentMethod
{
    [Required] public Guid Id { get; set; }
    [Required] public string? Name { get; set; }
}
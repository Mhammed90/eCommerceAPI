using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.Cart;

public class ProcessCart
{
    [Required] public Guid ProductId { get; set; }
    [Required] public int Quantity { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Domain.Entities.Cart;

public class Achieve
{
    [Key] public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
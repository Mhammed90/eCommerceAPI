namespace eCommerce.Domain.Entities.Identity;

public class RefreshToken
{
    public Guid Id { get; set; }
    public string? UserId { get; set; }
    public string? Token { get; set; }
}
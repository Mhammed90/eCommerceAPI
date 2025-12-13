using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.Identity;

public class BaseModel
{
    [Required] public string? Email { get; set; }
    [Required] public string? Password { get; set; }
}
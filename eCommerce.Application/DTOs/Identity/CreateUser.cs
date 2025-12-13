using System.ComponentModel.DataAnnotations;

namespace eCommerce.Application.DTOs.Identity;

public class CreateUser : BaseModel
{
    [Required] public string? FullName { get; set; } 
    [Required] public string? ConfirmPassword { get; set; }

}
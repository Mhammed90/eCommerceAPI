using eCommerce.Application.DTOs;
using FluentValidation;

namespace eCommerce.Application.Validation;

public class ValidationService : IValidationService
{
    public async Task<ServiceResponse> ValidateUser<T>(T model, IValidator<T> validator)
    {
        var validation = await validator.ValidateAsync(model);
        if (!validation.IsValid)
        {
            var errors = validation.Errors.Select(error => error.ErrorMessage).ToList();
            string message = string.Join("; ", errors);
            return new ServiceResponse { Message = message, Success = false };
        }
        return new ServiceResponse { Success = true };
    }
}
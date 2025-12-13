using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Identity;
using FluentValidation;

namespace eCommerce.Application.Validation;

public interface IValidationService
{
    Task<ServiceResponse> ValidateUser<T>(T model, IValidator<T> validator);
}
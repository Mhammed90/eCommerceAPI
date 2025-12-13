using eCommerce.Application.Mapping;
using eCommerce.Application.Services.Implementations;
using eCommerce.Application.Services.Interfaces;
using eCommerce.Application.Validation;
using eCommerce.Application.Validation.Authentication;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using eCommerce.Application.Services.Implementations.Authentication;
using eCommerce.Application.Services.Implementations.Cart;
using eCommerce.Application.Services.Interfaces.Authentication;
using eCommerce.Application.Services.Interfaces.Cart;


namespace eCommerce.Application.DependencyInjection;

public static class ServiceContainer
{
    public static IServiceCollection AddingApplicationService(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfile));
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddFluentValidationAutoValidation();
        // services.AddValidatorsFromAssemblyContaining(typeof(CreateUserValidator)); //they do the same
        services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
        // services.AddValidatorsFromAssemblyContaining(typeof(LoginUserValidator));
        services.AddScoped<IValidationService, ValidationService>();
        services.AddScoped<IAuthentciationService, AuthentciationService>();
        services.AddScoped<IPaymentMethodService, PaymentMethodService>();

        return services;
    }
}
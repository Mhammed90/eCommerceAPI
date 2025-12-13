using System.Text;
using eCommerce.Application.Mapping;
using eCommerce.Application.Services.Implementations.Cart;
using eCommerce.Application.Services.Interfaces.Cart;
using eCommerce.Application.Services.Logging;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Entities.Identity;
using eCommerce.Domain.Interfaces;
using eCommerce.Domain.Interfaces.Authentication;
using eCommerce.Domain.Interfaces.Cart;
using eCommerce.Infrastructure.Data;
using eCommerce.Infrastructure.Middleware;
using eCommerce.Infrastructure.Repositories;
using eCommerce.Infrastructure.Repositories.Authentication;
using eCommerce.Infrastructure.Repositories.Cart;
using eCommerce.Infrastructure.Services;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace eCommerce.Infrastructure.DependencyInjection;

public static class ServiceContainer
{
    public static IServiceCollection AddInfrastructureService(this IServiceCollection service,
        IConfiguration configuration)
    {
        service.AddDbContext<AppDbContext>(op =>
                op.UseSqlServer(configuration.GetConnectionString("defaultConnection")
                    , sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure();
                    }).UseExceptionProcessor(),
            ServiceLifetime.Scoped);

        service.AddScoped(typeof(IAppLogger<>), typeof(SerilogLoggerAdapter<>));

        service.AddDefaultIdentity<AppUser>(Options =>
            {
                Options.Password.RequireDigit = true;
                Options.Password.RequireLowercase = true;
                Options.Password.RequireUppercase = true;
                Options.Password.RequireNonAlphanumeric = true;
                Options.Password.RequiredLength = 6;
                Options.SignIn.RequireConfirmedEmail = true;
                Options.User.RequireUniqueEmail = true;
                Options.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultEmailProvider;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();

        service.AddAuthentication(Options =>
        {
            Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            Options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(Options =>
        {
            Options.SaveToken = true;
            Options.RequireHttpsMetadata = true;
            Options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                RequireSignedTokens = true,
                ValidIssuer = configuration["Jwt:Issuer"],
                ValidAudience = configuration["Jwt:Audience"],
                ClockSkew = TimeSpan.Zero,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
            };
        });
        service.AddScoped<IGeneric<Product>, GenericRepository<Product>>();
        service.AddScoped<IGeneric<Category>, GenericRepository<Category>>();

        service.AddScoped<IUserManagement, UserManagement>();
        service.AddScoped<IRoleManagement, RoleManagement>();
        service.AddScoped<ITokenManagement, TokenManagement>();
        service.AddScoped<IPaymentMethod, PaymentMethodRepository>();
        service.AddScoped<IPaymentService, StripePaymentService>();
        Stripe.StripeConfiguration.ApiKey = configuration["Stripe:SecretKey"];

        return service;
    }

    public static IApplicationBuilder UseInfrastuctureService(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        return app;
    }
}
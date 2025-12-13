
using eCommerce.Application.Services.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Infrastructure.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var logger = context.RequestServices.GetRequiredService<IAppLogger<ExceptionHandlingMiddleware>>();

        try
        {
            await _next(context);
        }
        catch (DbUpdateException ex)
        {
            context.Response.ContentType = "application/json";
            var innerException = ex.InnerException as SqlException;
            if (ex.InnerException is SqlException)
            {
                if (innerException != null)
                    logger.LogError(innerException, "sql exception");
                switch (innerException.Number)
                {
                    case 2627:
                        context.Response.StatusCode = StatusCodes.Status409Conflict;
                        await context.Response.WriteAsync("Unique Constraint Violation");
                        break;
                    case 547:
                        context.Response.StatusCode = StatusCodes.Status409Conflict;
                        await context.Response.WriteAsync("Foreign key  Constraint Violation");
                        break;
                    case 515:
                        context.Response.StatusCode = StatusCodes.Status400BadRequest;
                        await context.Response.WriteAsync("Can not insert null");
                        break;
                    default:
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        await context.Response.WriteAsync("an error occured while processing your request.");
                        break;
                }
            }
            else
            {
                logger.LogError(ex, "related EFCore exception");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsync("an error occured while processing your request.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "UnKnown exception");
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("An error occured " + ex.Message);
        }
    }
}
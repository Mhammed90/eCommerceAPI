// using eCommerce.Application.DependencyInjection;   

using eCommerce.Application.DependencyInjection;
using eCommerce.Infrastructure.DependencyInjection;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration().Enrich.FromLogContext().WriteTo.Console().WriteTo
    .File("log/log.txt", rollingInterval: RollingInterval.Day).CreateLogger();

builder.Host.UseSerilog();
Log.Logger.Information("Application is building..................");
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.AllowAnyMethod()
            .WithOrigins("http://localhost:5000")
            .AllowAnyHeader());
});


builder.Services.AddingApplicationService();
builder.Services.AddInfrastructureService(builder.Configuration);

builder.Services.AddControllers();
try
{
    var app = builder.Build();

    app.UseCors();

// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


    app.UseSerilogRequestLogging();
    app.UseInfrastuctureService();

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();

    Log.Logger.Information("Application is running..................");
    app.Run();
}
catch (Exception exception)
{
    Log.Logger.Error(exception, "Application failed to start .................. ");
}
finally
{
    Log.CloseAndFlush();
}
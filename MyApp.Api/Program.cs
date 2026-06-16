//using MyApp.Api.Extensions;
using MyApp.Common.Core.Data;
using MyApp.Common.Infrastructure;
using MyApp.Endpoints.Abstractions;
using MyApp.CareerAdvancement.Endpoints;
//using MyApp.SIS.Endpoints;
using Scalar.AspNetCore;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

builder.Host.UseSerilog();
try
{
    Log.Information("Starting web host");



    // Mediator (source-generated, no reflection)
    builder.Services.AddMediator(opts => opts.ServiceLifetime = ServiceLifetime.Scoped);

    // OpenAPI / Scalar
    builder.Services.AddOpenApi();

    // Register all module APIs (adds IApiEndpoint impls + DbContexts + validators)
    builder.Services.AddCareerApis();
    builder.Services.AddCareerServices(builder.Configuration.GetConnectionString("Default") ?? "Data Source=localhost;Initial Catalog=MyAppDb;Integrated Security=True;Encrypt=False");

    builder.Services.AddAuthentication();
    builder.Services.AddAuthorization();

    builder.Services.AddHttpContextAccessor();
    builder.Services.AddScoped<IUserInfoAccessor, UserInfoAccessor>();

    WebApplication app = builder.Build();

    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
        app.MapScalarApiReference();
    }

    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();


    app.MapGet("/", IResult () => app.Environment.IsDevelopment()
            ? TypedResults.Redirect("/scalar/v1")
            : TypedResults.Text($"Running in {app.Environment.EnvironmentName} environment."))
        .AllowAnonymous()
        .ExcludeFromDescription();

    // Single call routes all registered IApiEndpoint mappers
    app.MapApiEndpoints("api");

    app.Run();

}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
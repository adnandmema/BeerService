using System.Diagnostics.CodeAnalysis;
using BeerService.WebApi.API;
using BeerService.WebApi.Authentication;
using BeerService.WebApi.CORS;
using BeerService.WebApi.ErrorHandling;
using BeerService.WebApi.Logging;
using BeerService.WebApi.Swagger;
using BeerService.WebApi.Versioning;
using BeerService.Infrastructure;
using BeerService.Application;

namespace BeerService.WebApi;

[ExcludeFromCodeCoverage]
public class Startup
{
    protected IConfiguration Configuration { get; }
    protected IWebHostEnvironment Environment { get; }

    public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        => (Configuration, Environment) = (configuration, environment);

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMyApi();
        services.AddMyApiAuthDeps();
        services.AddMyErrorHandling();
        services.AddMySwagger(Configuration);
        services.AddMyVersioning();
        services.AddMyCorsConfiguration(Configuration);
        services.AddMyInfrastructureDependencies(Configuration, Environment);
        services.AddMyApplicationDependencies();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseMyRequestLogging();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseMyCorsConfiguration();
        app.UseMySwagger(Configuration);
        app.UseMyInfrastructure(Configuration, Environment);
        app.UseMyApi();
    }
}
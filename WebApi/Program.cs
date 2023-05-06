using Microsoft.AspNetCore.Hosting;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using BeerService.WebApi.Logging;
using BeerService.Infrastructure;

namespace BeerService.WebApi;

[ExcludeFromCodeCoverage]
public static class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Host
            .AddMySerilogLogging() // Notice: Logging overrides.
            .ConfigureAppConfiguration((context, config) =>
            {
                // ConfigureWebHostDefaults only adds secrets if environment is Develop.
                // This ensures they're always added, for local testing of Production setting.
                config.AddUserSecrets(Assembly.GetEntryAssembly(), optional: true);

                // Notice: Infrastructure hook.
                config.AddMyInfrastructureConfiguration(context);
            });

        var startup = new Startup(builder.Configuration, builder.Environment);

        startup.ConfigureServices(builder.Services);

        var app = builder.Build();

        startup.Configure(app);

        app.Run();
    }
}

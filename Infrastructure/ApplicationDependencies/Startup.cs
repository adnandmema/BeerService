using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerService.Application.Common.Dependencies.DataAccess;
using BeerService.Application.Common.Dependencies.DataAccess.Repositories;
using BeerService.Application.Common.Dependencies.Services;
using BeerService.Infrastructure.ApplicationDependencies.DataAccess;
using BeerService.Infrastructure.ApplicationDependencies.DataAccess.Repositories;
using BeerService.Infrastructure.ApplicationDependencies.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BeerService.Infrastructure.ApplicationDependencies;

[ExcludeFromCodeCoverage]
internal static class Startup
{
    public static void ConfigureServices(this IServiceCollection services, IConfiguration _)
    {
        services.AddScoped<IBeerRepository, BeerRepositoryEF>();
        services.AddScoped<IBeerRatingRepository, BeerRatingRepositoryEF>();
        services.AddScoped<IBeerTypeRepository, BeerTypeRepositoryEF>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddTransient<IDateTime, DateTimeService>();
    }
}

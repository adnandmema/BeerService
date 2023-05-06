using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BeerService.Infrastructure.Authorization;

// TODO: Implement authorization system, actually preferably in Application layer; if a good, simple use case can be established in this sample project.
[ExcludeFromCodeCoverage]
internal static class Startup
{
    public static void ConfigureServices(IServiceCollection services, IConfiguration _)
    {
    }
}
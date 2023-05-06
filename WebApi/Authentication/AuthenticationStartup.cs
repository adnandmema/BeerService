using System.Diagnostics.CodeAnalysis;
using BeerService.Application.Common.Dependencies.Services;
using BeerService.WebApi.Authentication.Services;

namespace BeerService.WebApi.Authentication;

[ExcludeFromCodeCoverage]
internal static class AuthenticationStartup
{
    public static void AddMyApiAuthDeps(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
    }
}

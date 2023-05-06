using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Identity;
using BeerService.Infrastructure.AzureKeyVault.Settings;
using BeerService.Infrastructure.Common.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace BeerService.Infrastructure.AzureKeyVault;

[ExcludeFromCodeCoverage]
internal static class Startup
{
    public static void ConfigureAppConfiguration(HostBuilderContext _, IConfigurationBuilder configBuilder)
    {
        var settings = configBuilder.Build().GetMyOptions<AzureKeyVaultSettings>();

        if (settings is not null && settings.AddToConfiguration)
        {
            configBuilder.AddAzureKeyVault(new Uri(settings.ServiceUrl), new DefaultAzureCredential());
        }
    }
}

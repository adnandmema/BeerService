using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerService.Infrastructure.Common.Validation;

namespace BeerService.Infrastructure.AzureKeyVault.Settings;

internal class AzureKeyVaultSettings
{
    [RequiredIf(nameof(AddToConfiguration), true)]
    public string? ServiceUrl { get; init; }

    [MemberNotNullWhen(true, nameof(ServiceUrl))]
    public bool AddToConfiguration { get; init; }
}

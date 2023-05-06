using System.ComponentModel.DataAnnotations;
using BeerService.Infrastructure.Authentication.External.Model;

namespace BeerService.WebApi.Authentication.Dtos;

public record ExternalLoginDto
{
    public ExternalAuthenticationProvider Provider { get; init; }

    [Required, MinLength(1)]
    public string IdToken { get; init; } = null!;
}
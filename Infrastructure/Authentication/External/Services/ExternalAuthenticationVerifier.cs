using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth;
using BeerService.Infrastructure.Authentication.External.Exceptions;
using BeerService.Infrastructure.Authentication.External.Model;
using BeerService.Infrastructure.Authentication.Settings;

namespace BeerService.Infrastructure.Authentication.External.Services;

public class ExternalAuthenticationVerifier : IExternalAuthenticationVerifier
{
    private readonly ExternalAuthenticationSettings _externalAuthSettings;

    public ExternalAuthenticationVerifier(ExternalAuthenticationSettings settings)
    {
        _externalAuthSettings = settings;
    }

    public Task<(bool success, ExternalUserData? userData)> Verify(ExternalAuthenticationProvider provider, string idToken)
        => provider switch
        {
            ExternalAuthenticationProvider.Google => AuthenticateGoogleToken(idToken),
            _ => throw new InvalidEnumArgumentException($"Support for provider '{provider}' is not implemented.")
        };

    private async Task<(bool success, ExternalUserData? userData)> AuthenticateGoogleToken(string idToken)
    {
        if (string.IsNullOrWhiteSpace(_externalAuthSettings.GoogleClientId))
        {
            throw new ExternalAuthenticationSetupException(provider: "Google");
        }

        GoogleJsonWebSignature.Payload data;
        try
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new[] { _externalAuthSettings.GoogleClientId }
            };
            data = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
        }
        catch (Exception ex)
        {
            if (ex is InvalidJwtException)
            {
                return (false, null);
            }
            else
            {
                throw new ExternalAuthenticationPreventedException(ex);
            }
        }

        return (
            success: true,
            userData: new ExternalUserData()
            {
                Email = data.Email,
                EmailVerified = data.EmailVerified,
                FullName = data.Name,
                LastName = data.FamilyName,
                FirstName = data.GivenName
            }
        );
    }
}

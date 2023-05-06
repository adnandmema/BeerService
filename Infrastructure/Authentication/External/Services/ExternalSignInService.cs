using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerService.Infrastructure.Authentication.Core.Model;
using BeerService.Infrastructure.Authentication.Core.Services;
using BeerService.Infrastructure.Authentication.External.Exceptions;
using BeerService.Infrastructure.Authentication.External.Model;

namespace BeerService.Infrastructure.Authentication.External.Services;

public class ExternalSignInService : IExternalSignInService
{
    private readonly IExternalAuthenticationVerifier _verifier;
    private readonly ITokenService _tokenService;

    public ExternalSignInService(IExternalAuthenticationVerifier verifier, ITokenService tokenService)
    {
        _verifier = verifier;
        _tokenService = tokenService;
    }

    public async Task<(MySignInResult result, SignInData? data)> SignInExternal(ExternalAuthenticationProvider provider, string idToken)
    {
        var (success, userData) = await _verifier.Verify(provider, idToken);

        if (!success)
        {
            return (MySignInResult.Failed, null);
        }

        if (string.IsNullOrWhiteSpace(userData!.Email) || string.IsNullOrWhiteSpace(userData.FullName))
        {
            var missingFields = new List<string>();
            if (string.IsNullOrWhiteSpace(userData.Email)) missingFields.Add(nameof(ExternalUserData.Email));
            if (string.IsNullOrWhiteSpace(userData.FullName)) missingFields.Add(nameof(ExternalUserData.FullName));

            throw new ExternalAuthenticationInfoException(
                missingFields: missingFields,
                receivedData: userData
            );
        }

        // Currently, in this sample app, we're not creating new local users for externally authenticated users.
        // Partically because that might involve privacy concerns.
        // TODO: Consider if user creation is needed, or if external logins should be registered in Identity tables.

        var token = _tokenService.CreateAuthenticationToken(
            userId: $"ext:{provider}:{userData.Email}",
            uniqueName: $"{userData.FullName} ({provider})");

        return (
            result: MySignInResult.Success,
            data: new SignInData()
            {
                ExternalAuthenticationProvider = provider.ToString(),
                Username = userData.FullName,
                Email = userData.Email,
                Token = token
            }
        );
    }
}

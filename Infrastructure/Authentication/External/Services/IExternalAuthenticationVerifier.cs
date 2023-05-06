using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerService.Infrastructure.Authentication.External.Model;

namespace BeerService.Infrastructure.Authentication.External.Services;

public interface IExternalAuthenticationVerifier
{
    Task<(bool success, ExternalUserData? userData)> Verify(ExternalAuthenticationProvider provider, string idToken);
}
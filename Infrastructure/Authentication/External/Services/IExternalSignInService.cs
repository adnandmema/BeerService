using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerService.Infrastructure.Authentication.Core.Model;
using BeerService.Infrastructure.Authentication.External.Model;

namespace BeerService.Infrastructure.Authentication.External.Services;

public interface IExternalSignInService
{
    Task<(MySignInResult result, SignInData? data)> SignInExternal(ExternalAuthenticationProvider provider, string idToken);
}
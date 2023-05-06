using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerService.Infrastructure.Authentication.External.Exceptions;

public class ExternalAuthenticationSetupException : Exception
{
    public ExternalAuthenticationSetupException(string provider)
        : base($"External provider '{provider}' is not set up properly for authentication.")
    { }
}

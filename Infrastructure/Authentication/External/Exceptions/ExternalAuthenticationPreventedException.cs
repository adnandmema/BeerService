using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerService.Infrastructure.Authentication.External.Exceptions;

public class ExternalAuthenticationPreventedException : Exception
{
    public ExternalAuthenticationPreventedException(Exception innerException)
        : base("Could not successfully execute authentication check with external provider. Maybe their services are not accessible currently.",
              innerException)
    { }
}

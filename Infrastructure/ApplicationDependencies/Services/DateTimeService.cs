using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerService.Application.Common.Dependencies.Services;

namespace BeerService.Infrastructure.ApplicationDependencies.Services;

internal class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}

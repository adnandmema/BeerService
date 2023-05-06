using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerService.Application.Common.Dependencies.Services;

public interface ICurrentUserService
{
    string UserId { get; }
}
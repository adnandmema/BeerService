using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerService.Infrastructure.Authentication.Core.Model;

namespace BeerService.Infrastructure.Authentication.Core.Services;

public interface IUserService
{
    Task<(MySignInResult result, SignInData? data)> SignIn(string username, string password);
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerService.Application.Common.Dependencies.DataAccess.Repositories.Common;
using BeerService.Domain.Entities;

namespace BeerService.Application.Common.Dependencies.DataAccess.Repositories;

public interface IBeerRatingRepository : IRepository<BeerRating>
{
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerService.Application.Common.Dependencies.DataAccess.Repositories;

namespace BeerService.Application.Common.Dependencies.DataAccess;

public interface IUnitOfWork : IDisposable
{
    public IBeerRepository Beers { get; }
    public IBeerTypeRepository BeerTypes { get; }
    public IBeerRatingRepository BeerRatings { get; }
    bool HasActiveTransaction { get; }

    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
    public Task SaveChanges();
}
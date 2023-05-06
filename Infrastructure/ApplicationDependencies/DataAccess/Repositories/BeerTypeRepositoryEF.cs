using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerService.Application.Common.Dependencies.DataAccess.Repositories;
using AutoMapper;
using BeerService.Domain.Entities;
using BeerService.Infrastructure.ApplicationDependencies.DataAccess.Repositories.Common;
using BeerService.Infrastructure.Persistence.Context;

namespace BeerService.Infrastructure.ApplicationDependencies.DataAccess.Repositories;

internal class BeerTypeRepositoryEF : RepositoryBaseEF<BeerType>, IBeerTypeRepository
{
    protected override IQueryable<BeerType> BaseQuery
        => _context.BeerTypes;

    public BeerTypeRepositoryEF(ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    { }

    public override void Remove(BeerType entityToDelete)
    {
        _context.Remove(entityToDelete);
    }

    public override void RemoveRange(IEnumerable<BeerType> entitiesToDelete)
    {
        foreach (var e in entitiesToDelete)
            Remove(e);
    }
}

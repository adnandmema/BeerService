using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerService.Application.Common.Dependencies.DataAccess;
using BeerService.Application.Common.Dependencies.DataAccess.Repositories.Common;
using MediatR;

namespace BeerService.Application.Beers.GetBeersList;

public class GetBeerListQueryHandler : IRequestHandler<ListQueryModel<BeerDto>, IListResponseModel<BeerDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetBeerListQueryHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public Task<IListResponseModel<BeerDto>> Handle(ListQueryModel<BeerDto> request, CancellationToken cancellationToken)
    {
        return _unitOfWork.Beers.GetProjectedListAsync(request, readOnly: true);
    }
}
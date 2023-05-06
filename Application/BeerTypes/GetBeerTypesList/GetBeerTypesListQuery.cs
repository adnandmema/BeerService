using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerService.Application.Common.Dependencies.DataAccess.Repositories.Common;
using BeerService.Application.Common.Dependencies.DataAccess;
using MediatR;

namespace BeerService.Application.BeerTypes.GetBeerTypesList;

public class GetBeerTypesListQueryHandler : IRequestHandler<ListQueryModel<BeerTypeDto>, IListResponseModel<BeerTypeDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetBeerTypesListQueryHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public Task<IListResponseModel<BeerTypeDto>> Handle(ListQueryModel<BeerTypeDto> request, CancellationToken cancellationToken)
        => _unitOfWork.BeerTypes.GetProjectedListAsync(request, readOnly: true);
}
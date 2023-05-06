using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BeerService.Application.Common.Dependencies.DataAccess;
using BeerService.Application.Common.Exceptions;
using BeerService.Domain.Entities;
using MediatR;

namespace BeerService.Application.BeerTypes.GetBeerTypeDetails;

public record GetBeerTypeDetailsQuery : IRequest<BeerTypeDetailsDto>
{
    public int Id { get; set; }
}

public class GetBeerTypeDetailsQueryHandler : IRequestHandler<GetBeerTypeDetailsQuery, BeerTypeDetailsDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBeerTypeDetailsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BeerTypeDetailsDto> Handle(GetBeerTypeDetailsQuery request, CancellationToken cancellationToken)
    {
        var beerType = await _unitOfWork.BeerTypes.GetByIdAsync(request.Id)
            ?? throw new EntityNotFoundException(nameof(BeerType), request.Id);

        return _mapper.Map<BeerTypeDetailsDto>(beerType);
    }
}

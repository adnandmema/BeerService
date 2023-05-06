using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using BeerService.Application.Common.Dependencies.DataAccess;
using BeerService.Application.Common.Exceptions;
using BeerService.Domain.Entities;
using MediatR;

namespace BeerService.Application.Beers.CreateBeer;

public record CreateBeerCommand : IRequest<int>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int BeerTypeId { get; set; }
}

public class CreateBeerCommandHandler : IRequestHandler<CreateBeerCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateBeerCommandHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<int> Handle(CreateBeerCommand request, CancellationToken cancellationToken)
    {
        bool beerType = await _unitOfWork.BeerTypes.ExistsAsync(request.BeerTypeId);

        if (!beerType)
            throw new InputValidationException((nameof(BeerType), $"BeerType (id: {request.BeerTypeId}) was not found."));

        var beer = new Beer(
            name: request.Name.Trim(),
            description: request.Description.Trim(),
            beerTypeId: request.BeerTypeId);

        _unitOfWork.Beers.Add(beer);
        await _unitOfWork.SaveChanges();

        return beer.Id;
    }
}
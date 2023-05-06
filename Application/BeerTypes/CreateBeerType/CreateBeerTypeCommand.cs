using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerService.Application.Common.Dependencies.DataAccess;
using BeerService.Domain.Entities;
using MediatR;

namespace BeerService.Application.BeerTypes.CreateBeerType;

public record CreateBeerTypeCommand : IRequest<int>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
}

public class CreateBeerTypeCommandHandler : IRequestHandler<CreateBeerTypeCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateBeerTypeCommandHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<int> Handle(CreateBeerTypeCommand request, CancellationToken cancellationToken)
    {
        var beerType = new BeerType(
            name: request.Name.Trim(),
            description: request.Description.Trim());

        _unitOfWork.BeerTypes.Add(beerType);
        await _unitOfWork.SaveChanges();

        return beerType.Id;
    }
}

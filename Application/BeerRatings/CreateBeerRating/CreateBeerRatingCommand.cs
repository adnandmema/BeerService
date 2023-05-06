using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerService.Application.Common.Dependencies.DataAccess;
using BeerService.Application.Common.Exceptions;
using BeerService.Domain.Entities;
using MediatR;

namespace BeerService.Application.BeerRatings.CreateBeerRating;

public record CreateBeerRatingCommand : IRequest<int>
{
    public int BeerId { get; set; }
    public RatingEnum Rating { get; set; }
}

public class CreateBeerRatingCommandHandler : IRequestHandler<CreateBeerRatingCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateBeerRatingCommandHandler(IUnitOfWork unitOfWork)
        => _unitOfWork = unitOfWork;

    public async Task<int> Handle(CreateBeerRatingCommand request, CancellationToken cancellationToken)
    {
        var beer = await _unitOfWork.Beers.GetByIdAsync(request.BeerId);

        if (beer is null)
            throw new InputValidationException((nameof(Beer), $"Beer (id: {request.BeerId}) was not found."));

        var beerRating = new BeerRating(
            beerId: request.BeerId,
            rating: request.Rating);

        var ratings = await _unitOfWork.BeerRatings.GetFiltered(x => x.BeerId == request.BeerId);
        double totalRatingSum = ratings is null 
            ? (int)request.Rating 
            : ratings.Sum(x => (int)x.Rating) + (int)request.Rating;
        int totalRatingCount = ratings is null
            ? 1
            : ratings.Count() + 1;

        var newAverageRating = totalRatingSum / totalRatingCount;

        _unitOfWork.BeerRatings.Add(beerRating);
        beer.AverageRating = newAverageRating;

        await _unitOfWork.SaveChanges();

        return beerRating.Id;
    }
}
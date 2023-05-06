using BeerService.Application.BeerRatings.CreateBeerRating;
using BeerService.Application.BeerTypes.CreateBeerType;
using BeerService.Application.BeerTypes.GetBeerTypeDetails;
using BeerService.Application.BeerTypes.GetBeerTypesList;
using BeerService.Application.Common.Dependencies.DataAccess.Repositories.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeerService.WebApi.API.V1;


[Authorize]
[ApiController]
[ApiVersion("1.0")]
[Route("v{v:apiVersion}/beerratings")]
public class BeerRatingsController : ControllerBase
{
    private readonly IMediator _mediator;

    public BeerRatingsController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateBeerRatingCommand command)
        => Ok(await _mediator.Send(command));
}
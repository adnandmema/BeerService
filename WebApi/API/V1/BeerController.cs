using BeerService.Application.Beers.CreateBeer;
using BeerService.Application.Beers.GetBeersList;
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
[Route("v{v:apiVersion}/beers")]
public class BeerController : ControllerBase
{
    private readonly IMediator _mediator;

    public BeerController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateBeerCommand command)
        => Ok(await _mediator.Send(command));

    [HttpGet]
    public async Task<ActionResult<IListResponseModel<BeerDto>>> GetList([FromQuery] ListQueryModel<BeerDto> query)
        => Ok(await _mediator.Send(query));

    //[HttpGet("{id}")]
    //[HttpGet("{id}")]
    //public async Task<ActionResult<BeerDetailsDto>> Get(int id)
    //    => Ok(await _mediator.Send(new GetBeerDetailsQuery() { Id = id }));
}

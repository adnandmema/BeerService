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
[Route("v{v:apiVersion}/beertypes")]
public class BeerTypeController : ControllerBase
{
    private readonly IMediator _mediator;

    public BeerTypeController(IMediator mediator) => _mediator = mediator;

    [HttpPost]
    public async Task<ActionResult<int>> Create(CreateBeerTypeCommand command)
        => Ok(await _mediator.Send(command));

    [HttpGet]
    public async Task<ActionResult<IListResponseModel<BeerTypeDto>>> GetList([FromQuery] ListQueryModel<BeerTypeDto> query)
        => Ok(await _mediator.Send(query));

    [HttpGet("{id}")]
    public async Task<ActionResult<BeerTypeDetailsDto>> Get(int id)
        => Ok(await _mediator.Send(new GetBeerTypeDetailsQuery() { Id = id }));
}
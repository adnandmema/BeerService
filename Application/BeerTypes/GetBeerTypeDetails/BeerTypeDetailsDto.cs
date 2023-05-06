using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BeerService.Application.Common.Mapping;
using BeerService.Domain.Entities;

namespace BeerService.Application.BeerTypes.GetBeerTypeDetails;

public record BeerTypeDetailsDto : IMapFrom<BeerType>
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; }
    public DateTime CreatedAt { get; init; }
    public DateTime? LastModifiedAt { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<BeerType, BeerTypeDetailsDto>();
    }
}

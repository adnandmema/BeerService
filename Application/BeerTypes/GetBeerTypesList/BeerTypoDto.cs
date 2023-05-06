using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BeerService.Application.Common.Mapping;
using BeerService.Domain.Entities;

namespace BeerService.Application.BeerTypes.GetBeerTypesList;

public record BeerTypeDto : IMapFrom<BeerType>
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<BeerType, BeerTypeDto>();
    }
}
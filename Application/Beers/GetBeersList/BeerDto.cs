using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BeerService.Application.Common.Mapping;
using BeerService.Domain.Entities;

namespace BeerService.Application.Beers.GetBeersList;

public record BeerDto : IMapFrom<Beer>
{
    public int Id { get; init; }
    public string Name { get; init; } = null!;
    public string BeerType { get; init; } = null!;
    public double AverageRating { get; set; }
    public string Description { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Beer, BeerDto>()
            .ForMember(dest => dest.BeerType, x => x.MapFrom(src => src.BeerType.Name));
    }
}

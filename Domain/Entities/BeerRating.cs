using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerService.Domain.Common;

namespace BeerService.Domain.Entities;
public class BeerRating : MyEntity
{
    public int BeerId { get; private set; }
    public virtual Beer Beer { get; private set; }
    public RatingEnum Rating { get; set; }

    public BeerRating()
    {
    }

    public BeerRating(int beerId, RatingEnum rating)
    {
        BeerId = beerId;
        Rating = rating;
    }
}

public enum RatingEnum
{
    Poor = 1,
    Fair = 2,
    Good = 3,
    VeryGood = 4,
    Excellent = 5
}

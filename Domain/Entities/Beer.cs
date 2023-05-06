using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using BeerService.Domain.Common;

namespace BeerService.Domain.Entities;
public class Beer : MyEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int BeerTypeId { get; private set; }
    public virtual BeerType BeerType { get; private set; }
    public double AverageRating { get; set; }

    public Beer(string name, string description, int beerTypeId)
    {
        Name = name;
        Description = description;
        BeerTypeId = beerTypeId;
    }
}

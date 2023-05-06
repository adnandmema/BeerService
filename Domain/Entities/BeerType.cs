using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerService.Domain.Common;

namespace BeerService.Domain.Entities;
public class BeerType : MyEntity
{
    public string Name { get; init; }
    public string Description { get; set; }

    public BeerType(string name, string description)
    {
        Name = name;
        Description = description;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeerService.Application.Beers.CreateBeer;
using FluentValidation;

namespace BeerService.Application.BeerTypes.CreateBeerType;

public class CreateBeerTypeCommandValidator : AbstractValidator<CreateBeerTypeCommand>
{
    public CreateBeerTypeCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace BeerService.Application.Beers.CreateBeer;

public class CreateBeerCommandValidator : AbstractValidator<CreateBeerCommand>
{
    public CreateBeerCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
    }
}

﻿using System.ComponentModel.DataAnnotations;

namespace BeerService.WebApi.Swagger.Configuration;

class SwaggerSettings
{
    [Required, MinLength(1)]
    public string ApiName { get; init; } = null!;

    public bool UseSwagger { get; init; }

    [Required, MinLength(1)]
    public string LoginPath { get; set; } = null!;
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerService.Domain.Common;
public interface ISoftDeletable
{
    public string? DeletedBy { get; }

    public DateTime? DeletedAt { get; }
}
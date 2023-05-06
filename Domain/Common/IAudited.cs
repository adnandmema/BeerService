using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerService.Domain.Common;
public interface IAudited
{
    string CreatedBy { get; }

    DateTime CreatedAt { get; }

    string? LastModifiedBy { get; }

    DateTime? LastModifiedAt { get; }
}

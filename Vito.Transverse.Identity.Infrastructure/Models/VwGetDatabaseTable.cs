using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class VwGetDatabaseTable
{
    public long? NameRowNumber { get; set; }

    public string Name { get; set; } = null!;

    public string Expr1 { get; set; } = null!;

    public int IsActive { get; set; }
}

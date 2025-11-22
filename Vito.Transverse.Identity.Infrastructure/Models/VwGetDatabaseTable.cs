using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

[Keyless]
public partial class VwGetDatabaseTable
{
    [Column("name_row_number")]
    public long? NameRowNumber { get; set; }

    [Column("name")]
    [StringLength(128)]
    public string Name { get; set; } = null!;

    [StringLength(128)]
    public string Expr1 { get; set; } = null!;

    public int IsActive { get; set; }
}

using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class Application
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid Secret { get; set; }

    public bool IsActive { get; set; }

    public byte[]? Avatar { get; set; }

    public virtual ICollection<Company> CompanyFks { get; set; } = new List<Company>();
}

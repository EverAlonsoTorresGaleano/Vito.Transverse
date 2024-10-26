using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class Company
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Subdomain { get; set; } = null!;

    public string Email { get; set; } = null!;

    public Guid Secret { get; set; }

    public bool IsActive { get; set; }

    public byte[]? Avatar { get; set; }

    public string DefaultCultureFk { get; set; } = null!;

    public string CountryFk { get; set; } = null!;

    public bool IsSystemCompany { get; set; }

    public virtual Country CountryFkNavigation { get; set; } = null!;

    public virtual Culture DefaultCultureFkNavigation { get; set; } = null!;

    public virtual ICollection<Person> People { get; set; } = new List<Person>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    public virtual ICollection<Application> ApplicationFks { get; set; } = new List<Application>();
}

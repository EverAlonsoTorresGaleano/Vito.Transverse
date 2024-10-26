using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class Person
{
    public Guid CompanyFk { get; set; }

    public long Id { get; set; }

    public long DocumentTypeFk { get; set; }

    public string DocumentValue { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public long GenderFk { get; set; }

    public string? MobileNumber { get; set; }

    public byte[]? Avatar { get; set; }

    public virtual Company CompanyFkNavigation { get; set; } = null!;

    public virtual ListItem DocumentTypeFkNavigation { get; set; } = null!;

    public virtual ListItem GenderFkNavigation { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}

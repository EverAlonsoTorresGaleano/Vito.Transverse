using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class User
{
    public Guid CompanyFk { get; set; }

    public long Id { get; set; }

    public string UserName { get; set; } = null!;

    public long PersonFk { get; set; }

    public string Password { get; set; } = null!;

    public long RoleFk { get; set; }

    public bool EmailValidated { get; set; }

    public bool RequirePasswordChange { get; set; }

    public int RetryCount { get; set; }

    public DateTime? LastAccess { get; set; }

    public bool ActivationEmailSent { get; set; }

    public Guid ActivationId { get; set; }

    public bool IsActive { get; set; }

    public bool IsLocked { get; set; }

    public DateTime? LockedDate { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual Company CompanyFkNavigation { get; set; } = null!;

    public virtual Person PersonFkNavigation { get; set; } = null!;

    public virtual Role RoleFkNavigation { get; set; } = null!;
}

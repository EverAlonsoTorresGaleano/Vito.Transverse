using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class User
{
    public long CompanyFk { get; set; }

    public string UserName { get; set; } = null!;

    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool EmailValidated { get; set; }

    public bool RequirePasswordChange { get; set; }

    public int RetryCount { get; set; }

    public DateTime? LastAccess { get; set; }

    public bool ActivationEmailSent { get; set; }

    public Guid ActivationId { get; set; }

    public bool IsLocked { get; set; }

    public DateTime? LockedDate { get; set; }

    public DateTime? CreationDate { get; set; }

    public long? CreatedByUserFk { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public long? UpdatedByUserFk { get; set; }

    public byte[]? Avatar { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    public virtual Company CompanyFkNavigation { get; set; } = null!;

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class User
{
    public long CompanyFk { get; set; }

    [StringLength(30)]
    public string UserName { get; set; } = null!;

    [Key]
    public long Id { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Unicode(false)]
    public string Password { get; set; } = null!;

    public bool EmailValidated { get; set; }

    public bool RequirePasswordChange { get; set; }

    public int RetryCount { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastAccess { get; set; }

    public bool ActivationEmailSent { get; set; }

    public Guid ActivationId { get; set; }

    public bool IsLocked { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LockedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public long CreatedByUserFk { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastUpdatedDate { get; set; }

    public long? LastUpdatedByUserFk { get; set; }

    public byte[]? Avatar { get; set; }

    public bool IsActive { get; set; }

    [InverseProperty("UserFkNavigation")]
    public virtual ICollection<ActivityLog> ActivityLogs { get; set; } = new List<ActivityLog>();

    [InverseProperty("UserFkNavigation")]
    public virtual ICollection<AuditRecord> AuditRecords { get; set; } = new List<AuditRecord>();

    [ForeignKey("CompanyFk")]
    [InverseProperty("Users")]
    public virtual Company CompanyFkNavigation { get; set; } = null!;

    [InverseProperty("UserFkNavigation")]
    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class CompanyEntityAudit
{
    public long CompanyFk { get; set; }

    [Key]
    public long Id { get; set; }

    public long EntityFk { get; set; }

    public long AuditTypeFk { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    public long CreatedByUserFk { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastUpdatedDate { get; set; }

    public long? LastUpdatedByUserFk { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("AuditTypeFk")]
    [InverseProperty("CompanyEntityAudits")]
    public virtual GeneralTypeItem AuditTypeFkNavigation { get; set; } = null!;

    [ForeignKey("CompanyFk")]
    [InverseProperty("CompanyEntityAudits")]
    public virtual Company CompanyFkNavigation { get; set; } = null!;

    [ForeignKey("EntityFk")]
    [InverseProperty("CompanyEntityAudits")]
    public virtual Entity EntityFkNavigation { get; set; } = null!;
}

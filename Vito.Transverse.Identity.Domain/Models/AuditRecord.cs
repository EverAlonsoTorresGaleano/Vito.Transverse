using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class AuditRecord
{
    public long CompanyFk { get; set; }

    public long Id { get; set; }

    public long UserFk { get; set; }

    public long AuditEntityFk { get; set; }

    public long AuditTypeFk { get; set; }

    public string AuditEntityIndex { get; set; } = null!;

    public string HostName { get; set; } = null!;

    public string IpAddress { get; set; } = null!;

    public string? DeviceType { get; set; }

    public string Browser { get; set; } = null!;

    public string Platform { get; set; } = null!;

    public string Engine { get; set; } = null!;

    public string CultureFk { get; set; } = null!;

    public string AuditInfoJson { get; set; } = null!;

    public DateTime CreationDate { get; set; }

    public virtual AuditEntity AuditEntityFkNavigation { get; set; } = null!;

    public virtual GeneralTypeItem AuditTypeFkNavigation { get; set; } = null!;
}

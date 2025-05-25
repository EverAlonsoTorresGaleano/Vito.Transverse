using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class AuditRecord
{
    public long CompanyFk { get; set; }

    public long Id { get; set; }

    public long UserFk { get; set; }

    public long EntityFk { get; set; }

    public long AuditTypeFk { get; set; }

    public string AuditEntityIndex { get; set; } = null!;

    public string HostName { get; set; } = null!;

    public string IpAddress { get; set; } = null!;

    public string DeviceType { get; set; } = null!;

    public string Browser { get; set; } = null!;

    public string Platform { get; set; } = null!;

    public string Engine { get; set; } = null!;

    public string CultureFk { get; set; } = null!;

    public string EndPointUrl { get; set; } = null!;

    public string Method { get; set; } = null!;

    public string QueryString { get; set; } = null!;

    public string UserAgent { get; set; } = null!;

    public string Referer { get; set; } = null!;

    public long ApplicationId { get; set; }

    public long RoleId { get; set; }

    public DateTime CreationDate { get; set; }

    public string AuditChanges { get; set; } = null!;

    public virtual GeneralTypeItem AuditTypeFkNavigation { get; set; } = null!;

    public virtual Company CompanyFkNavigation { get; set; } = null!;

    public virtual Entity EntityFkNavigation { get; set; } = null!;

    public virtual User UserFkNavigation { get; set; } = null!;
}

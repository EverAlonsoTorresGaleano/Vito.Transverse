using System;
using System.Collections.Generic;

namespace  Vito.Transverse.Identity.Infrastructure.Models;

public partial class ActivityLog
{
    public long CompanyFk { get; set; }

    public long UserFk { get; set; }

    public long TraceId { get; set; }

    public DateTime EventDate { get; set; }

    public string DeviceName { get; set; } = null!;

    public string DeviceType { get; set; } = null!;

    public long ActionTypeFk { get; set; }

    public string IpAddress { get; set; } = null!;

    public string Browser { get; set; } = null!;

    public string Platform { get; set; } = null!;

    public string Engine { get; set; } = null!;

    public string CultureId { get; set; } = null!;

    public string EndPointUrl { get; set; } = null!;

    public string Method { get; set; } = null!;

    public string QueryString { get; set; } = null!;

    public string UserAgent { get; set; } = null!;

    public string Referer { get; set; } = null!;

    public long ApplicationId { get; set; }

    public long RoleId { get; set; }

    public virtual GeneralTypeItem ActionTypeFkNavigation { get; set; } = null!;

    public virtual User UserFkNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace  Vito.Transverse.Identity.Infrastructure.Models;

public partial class VwGetAuditRecord
{
    public DateTime CreationDate { get; set; }

    public long CompanyFk { get; set; }

    public string NameTranslationKey { get; set; } = null!;

    public long UserFk { get; set; }

    public string UserName { get; set; } = null!;

    public long AuditTypeFk { get; set; }

    public string Expr1 { get; set; } = null!;

    public long EntityFk { get; set; }

    public string SchemaName { get; set; } = null!;

    public string EntityName { get; set; } = null!;

    public bool IsSystemEntity { get; set; }

    public string HostName { get; set; } = null!;

    public string DeviceType { get; set; } = null!;

    public string Browser { get; set; } = null!;

    public string Platform { get; set; } = null!;

    public string Engine { get; set; } = null!;

    public string CultureFk { get; set; } = null!;

    public string AuditChanges { get; set; } = null!;
}

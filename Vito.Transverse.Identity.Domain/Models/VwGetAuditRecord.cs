using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class VwGetAuditRecord
{
    public DateTime CreationDate { get; set; }

    public long CompanyFk { get; set; }

    public string Name { get; set; } = null!;

    public long UserFk { get; set; }

    public string UserName { get; set; } = null!;

    public long AuditTypeFk { get; set; }

    public string NameTranslationKey { get; set; } = null!;

    public long AuditEntityFk { get; set; }

    public string SchemaName { get; set; } = null!;

    public string EntityName { get; set; } = null!;

    public bool IsSystemEntity { get; set; }

    public string HostName { get; set; } = null!;

    public string? DeviceType { get; set; }

    public string Browser { get; set; } = null!;

    public string Platform { get; set; } = null!;

    public string Engine { get; set; } = null!;

    public string CultureFk { get; set; } = null!;

    public string AuditInfoJson { get; set; } = null!;
}

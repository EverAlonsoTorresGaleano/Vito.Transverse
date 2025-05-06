using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class VwGetAllCompanyPermission
{
    public long CompanyMembershipFk { get; set; }

    public long CompanyMembershipPermissionsId { get; set; }

    public long CompanyId { get; set; }

    public string CompanyName { get; set; } = null!;

    public long ApplicationId { get; set; }

    public string ApplicationName { get; set; } = null!;

    public bool ApplicationIsActive { get; set; }

    public long ModuleId { get; set; }

    public string ModuleName { get; set; } = null!;

    public bool ModuleIsActive { get; set; }

    public long PageId { get; set; }

    public string PageName { get; set; } = null!;

    public bool PageIsActive { get; set; }

    public long? ComponentFk { get; set; }
}

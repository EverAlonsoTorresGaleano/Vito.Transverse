using System;
using System.Collections.Generic;

namespace  Vito.Transverse.Identity.Infrastructure.Models;

public partial class VwCompanyUserRole
{
    public long CompanyId { get; set; }

    public string CompanyName { get; set; } = null!;

    public long ApplicationId { get; set; }

    public string ApplicationName { get; set; } = null!;

    public long UserId { get; set; }

    public string UserName { get; set; } = null!;

    public long RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public bool UserRoleIsActive { get; set; }
}

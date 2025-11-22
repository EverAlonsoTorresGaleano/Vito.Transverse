using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

[Keyless]
public partial class VwCompanyUserRole
{
    public long CompanyId { get; set; }

    [StringLength(85)]
    [Unicode(false)]
    public string CompanyName { get; set; } = null!;

    public long ApplicationId { get; set; }

    [StringLength(85)]
    [Unicode(false)]
    public string ApplicationName { get; set; } = null!;

    public long UserId { get; set; }

    [StringLength(130)]
    public string UserName { get; set; } = null!;

    public long RoleId { get; set; }

    [StringLength(85)]
    [Unicode(false)]
    public string RoleName { get; set; } = null!;

    public bool UserRoleIsActive { get; set; }
}

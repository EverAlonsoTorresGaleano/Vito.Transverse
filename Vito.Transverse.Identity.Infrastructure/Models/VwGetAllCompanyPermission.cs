using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

[Keyless]
public partial class VwGetAllCompanyPermission
{
    public long CompanyMembershipFk { get; set; }

    public long CompanyMembershipPermissionsId { get; set; }

    public long CompanyId { get; set; }

    [StringLength(85)]
    [Unicode(false)]
    public string CompanyName { get; set; } = null!;

    public long ApplicationId { get; set; }

    [StringLength(85)]
    [Unicode(false)]
    public string ApplicationName { get; set; } = null!;

    public bool ApplicationIsActive { get; set; }

    public long ModuleId { get; set; }

    [StringLength(85)]
    [Unicode(false)]
    public string ModuleName { get; set; } = null!;

    public bool ModuleIsActive { get; set; }

    public long EndpointId { get; set; }

    [StringLength(85)]
    [Unicode(false)]
    public string EndpointName { get; set; } = null!;

    [StringLength(75)]
    [Unicode(false)]
    public string EndpointUrl { get; set; } = null!;

    public bool EndpointIsActive { get; set; }

    public long? ComponentFk { get; set; }
}

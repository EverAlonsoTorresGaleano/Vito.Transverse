using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public class CompanyMembershipPermissionDTO
{
    [Required]
    public long CompanyMembershipFk { get; set; }

    [Required]
    public long CompanyFk { get; set; }

    [Required]
    public long ApplicationFk { get; set; }

    [Required]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    public long ModuleFk { get; set; }

    [Required]
    public long EndpointFk { get; set; }

    public long? ComponentFk { get; set; }

    [StringLength(75)]
    [Unicode(false)]
    public string? PropertyValue { get; set; }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public record RolePermissionDTO
{
    [Required]
    public long RoleFk { get; set; }

    [Required]
    public long CompanyFk { get; set; }

    [Required]
    public long ApplicationFk { get; set; }

    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public long Id { get; set; }

    [Required]
    public long ModuleFk { get; set; }


    public long? EndpointFk { get; set; }

    public long? ComponentFk { get; set; }

    [StringLength(75)]
    [Unicode(false)]
    public string? PropertyValue { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? Obs { get; set; }

    [Required]
    public bool? CanView { get; set; }

    [Required]
    public bool? CanCreate { get; set; }

    [Required]
    public bool? CanEdit { get; set; }

    [Required]
    public bool? CanDelete { get; set; }
}

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public record EntityDTO
{
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public long Id { get; set; }

    [Required]
    [StringLength(75)]
    [Unicode(false)]
    public string SchemaName { get; set; } = null!;

    [Required]
    [StringLength(75)]
    [Unicode(false)]
    public string EntityName { get; set; } = null!;

    [Required]
    public bool IsActive { get; set; }

    [Required]    
    public bool IsSystemEntity { get; set; }
}

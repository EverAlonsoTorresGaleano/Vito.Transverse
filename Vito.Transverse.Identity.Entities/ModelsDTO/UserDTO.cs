

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vito.Framework.Common.Enums;

namespace Vito.Transverse.Identity.Entities.ModelsDTO;


public class UserDTO
{
    [Required]
    public long CompanyFk { get; set; }

    [Required]
    [StringLength(30)]
    public string UserName { get; set; } = null!;

    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Key]
    public long Id { get; set; }

    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [Required]
    [StringLength(100)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [NotMapped]
    [Unicode(false)]
    public string Password { get; set; } = null!;


    [NotMapped]
    public bool EmailValidated { get; set; }

    [Required]
    public bool RequirePasswordChange { get; set; }

    [NotMapped]
    public int RetryCount { get; set; }

    [NotMapped]
    [Column(TypeName = "datetime")]
    public DateTime? LastAccess { get; set; }

    [NotMapped]
    public bool ActivationEmailSent { get; set; }

    [NotMapped]
    public Guid ActivationId { get; set; }

    [Required]
    public bool IsLocked { get; set; }

    [NotMapped]
    [Column(TypeName = "datetime")]
    public DateTime? LockedDate { get; set; }


    public byte[]? Avatar { get; set; }

    [Required]
    public bool IsActive { get; set; }

    //Create Update Time and User

    [NotMapped]
    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [NotMapped]
    public long CreatedByUserFk { get; set; }


    [NotMapped]
    [Column(TypeName = "datetime")]
    public DateTime? LastUpdatedDate { get; set; }

    [NotMapped]
    public long? LastUpdatedByUserFk { get; set; }

    //Extension

    public List<RoleDTO> Roles { get; set; } = new();

    public string CompanyNameTranslationKey { get; set; } = null!;
    public object CompanyClient { get;  set; }=null!;

    public string NewPassword1 { get; set; } = null!;

    public string NewPassword2 { get; set; } = null!;
}
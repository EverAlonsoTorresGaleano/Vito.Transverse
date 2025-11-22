using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

[Index("CompanyFk", "Name", Name = "IX_Pictures")]
public partial class Picture
{
    public long CompanyFk { get; set; }

    [StringLength(75)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Key]
    public long Id { get; set; }

    public long? EntityFk { get; set; }

    public long FileTypeFk { get; set; }

    public long PictureCategoryFk { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreationDate { get; set; }

    public long CreatedByUserFk { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? LastUpdateDate { get; set; }

    public long? LastUpdateByUserFk { get; set; }

    public bool IsActive { get; set; }

    public byte[] BinaryPicture { get; set; } = null!;

    [Column(TypeName = "decimal(18, 5)")]
    public decimal PictureSize { get; set; }

    [ForeignKey("CompanyFk")]
    [InverseProperty("Pictures")]
    public virtual Company CompanyFkNavigation { get; set; } = null!;

    [ForeignKey("EntityFk")]
    [InverseProperty("Pictures")]
    public virtual Entity? EntityFkNavigation { get; set; }

    [ForeignKey("FileTypeFk")]
    [InverseProperty("PictureFileTypeFkNavigations")]
    public virtual GeneralTypeItem FileTypeFkNavigation { get; set; } = null!;

    [ForeignKey("PictureCategoryFk")]
    [InverseProperty("PicturePictureCategoryFkNavigations")]
    public virtual GeneralTypeItem PictureCategoryFkNavigation { get; set; } = null!;
}

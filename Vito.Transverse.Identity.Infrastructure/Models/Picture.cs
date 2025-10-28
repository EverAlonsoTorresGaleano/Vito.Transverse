using System;
using System.Collections.Generic;

namespace  Vito.Transverse.Identity.Infrastructure.Models;

public partial class Picture
{
    public long CompanyFk { get; set; }

    public string Name { get; set; } = null!;

    public long Id { get; set; }

    public long EntityFk { get; set; }

    public long FileTypeFk { get; set; }

    public long PictureCategoryFk { get; set; }

    public DateTime CreationDate { get; set; }

    public long CreatedByUserFk { get; set; }

    public DateTime? LastUpdateDate { get; set; }

    public long? LastUpdateByUserFk { get; set; }

    public bool IsActive { get; set; }

    public byte[]? BinaryPicture { get; set; }

    public decimal PictureSize { get; set; }

    public virtual Company CompanyFkNavigation { get; set; } = null!;

    public virtual Entity EntityFkNavigation { get; set; } = null!;

    public virtual GeneralTypeItem FileTypeFkNavigation { get; set; } = null!;

    public virtual GeneralTypeItem PictureCategoryFkNavigation { get; set; } = null!;
}

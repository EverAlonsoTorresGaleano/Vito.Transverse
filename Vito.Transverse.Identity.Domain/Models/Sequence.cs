using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class Sequence
{
    public long CompanyFk { get; set; }

    public long ApplicationFk { get; set; }

    public long SequenceTypeFk { get; set; }

    public long Id { get; set; }

    public string SequenceNameFormat { get; set; } = null!;

    public long SequenceIndex { get; set; }

    public string TextFormat { get; set; } = null!;

    public virtual Application ApplicationFkNavigation { get; set; } = null!;

    public virtual Company CompanyFkNavigation { get; set; } = null!;
}

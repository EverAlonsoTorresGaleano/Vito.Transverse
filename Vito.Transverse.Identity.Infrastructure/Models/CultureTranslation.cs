using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class CultureTranslation
{
    public long ApplicationFk { get; set; }

    public string CultureFk { get; set; } = null!;

    public string TranslationKey { get; set; } = null!;

    public string TranslationValue { get; set; } = null!;

    public virtual Application ApplicationFkNavigation { get; set; } = null!;

    public virtual Culture CultureFkNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class CultureTranslation
{
    public string CultureFk { get; set; } = null!;

    public string TranslationKey { get; set; } = null!;

    public string TranslationValue { get; set; } = null!;

    public virtual Culture CultureFkNavigation { get; set; } = null!;
}

using System;
using System.Collections.Generic;

namespace Vito.Transverse.Identity.Domain.Models;

public partial class TraceAction
{
    public long Id { get; set; }

    public string ActionTranslationKey { get; set; } = null!;

    public virtual ICollection<ActivityLog> UserTraces { get; set; } = new List<ActivityLog>();
}

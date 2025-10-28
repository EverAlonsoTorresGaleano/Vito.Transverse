using System;
using System.Collections.Generic;

namespace  Vito.Transverse.Identity.Infrastructure.Models;

public partial class Culture
{
    public string Id { get; set; } = null!;

    public string NameTranslationKey { get; set; } = null!;

    public string CountryFk { get; set; } = null!;

    public string LanguageFk { get; set; } = null!;

    public bool IsEnabled { get; set; }

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    public virtual Country CountryFkNavigation { get; set; } = null!;

    public virtual ICollection<CultureTranslation> CultureTranslations { get; set; } = new List<CultureTranslation>();

    public virtual Language LanguageFkNavigation { get; set; } = null!;

    public virtual ICollection<NotificationTemplate> NotificationTemplates { get; set; } = new List<NotificationTemplate>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}

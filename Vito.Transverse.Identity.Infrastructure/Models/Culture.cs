using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class Culture
{
    [Key]
    [StringLength(5)]
    [Unicode(false)]
    public string Id { get; set; } = null!;

    [StringLength(85)]
    [Unicode(false)]
    public string NameTranslationKey { get; set; } = null!;

    [StringLength(2)]
    [Unicode(false)]
    public string CountryFk { get; set; } = null!;

    [StringLength(2)]
    [Unicode(false)]
    public string LanguageFk { get; set; } = null!;

    public bool IsEnabled { get; set; }

    [InverseProperty("DefaultCultureFkNavigation")]
    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    [ForeignKey("CountryFk")]
    [InverseProperty("Cultures")]
    public virtual Country CountryFkNavigation { get; set; } = null!;

    [InverseProperty("CultureFkNavigation")]
    public virtual ICollection<CultureTranslation> CultureTranslations { get; set; } = new List<CultureTranslation>();

    [ForeignKey("LanguageFk")]
    [InverseProperty("Cultures")]
    public virtual Language LanguageFkNavigation { get; set; } = null!;

    [InverseProperty("CultureFkNavigation")]
    public virtual ICollection<NotificationTemplate> NotificationTemplates { get; set; } = new List<NotificationTemplate>();

    [InverseProperty("CultureFkNavigation")]
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
}

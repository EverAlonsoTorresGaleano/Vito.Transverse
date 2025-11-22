using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Vito.Transverse.Identity.Infrastructure.Models;

public partial class Sequence
{
    public long? CompanyFk { get; set; }

    public long? ApplicationFk { get; set; }

    public long SequenceTypeFk { get; set; }

    [Key]
    public long Id { get; set; }

    [StringLength(75)]
    [Unicode(false)]
    public string SequenceNameFormat { get; set; } = null!;

    public long SequenceIndex { get; set; }

    [StringLength(15)]
    [Unicode(false)]
    public string TextFormat { get; set; } = null!;

    [ForeignKey("ApplicationFk")]
    [InverseProperty("Sequences")]
    public virtual Application? ApplicationFkNavigation { get; set; }

    [ForeignKey("CompanyFk")]
    [InverseProperty("Sequences")]
    public virtual Company? CompanyFkNavigation { get; set; }

    [ForeignKey("SequenceTypeFk")]
    [InverseProperty("Sequences")]
    public virtual GeneralTypeItem SequenceTypeFkNavigation { get; set; } = null!;
}

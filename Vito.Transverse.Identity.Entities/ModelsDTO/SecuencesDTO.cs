using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vito.Transverse.Identity.Entities.ModelsDTO;

//developers
public record SequencesDTO
{
    public long? CompanyFk { get; set; }

    public long? ApplicationFk { get; set; }

    [Required]
    public long SequenceTypeFk { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required]
    [StringLength(75)]
    [Unicode(false)]
    public string SequenceNameFormat { get; set; } = null!;

    [Required]
    public long SequenceIndex { get; set; }

    [Required]
    [StringLength(15)]
    [Unicode(false)]
    public string TextFormat { get; set; } = null!;


    //Extension
    public string CompanyNameTranslationKey { get; set; } = null!;
    public string ApplicationNameTranslationKey { get; set; } = null!;
    public string SequenceTypeNameTranslationKey { get; set; } = null!;

}


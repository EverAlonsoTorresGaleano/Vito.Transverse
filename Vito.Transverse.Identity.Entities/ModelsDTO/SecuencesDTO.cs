namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public record SecuencesDTO
{
    public long Id { get; set; }

    public long CompanyId { get; set; }
    public string CompanyNameTranslationKey { get; set; } = null!;

    public long ApplicationId { get; set; }
    public string ApplicationNameTranslationKey { get; set; } = null!;

    public long SequenceTypeId { get; set; }
    public string SequenceTypeNameTranslationKey { get; set; } = null!;

    public string SequenceNameFormat { get; set; } = null!;

    public long SequenceIndex { get; set; }

    public string TextFormat { get; set; } = null!;
}


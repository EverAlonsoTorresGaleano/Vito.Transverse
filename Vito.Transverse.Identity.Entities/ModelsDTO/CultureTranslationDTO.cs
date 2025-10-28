namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public class CultureTranslationDTO
{
    public long ApplicationFk { get; set; }
    public string CultureFk { get; set; } = string.Empty;
    public string TranslationKey { get; set; } = string.Empty;
    public string TranslationValue { get; set; } = string.Empty;


    public string ApplicationNameTranslationKey { get; set; } = null!;
    public string LanguageNameTranslationKey { get; set; } = null!;
}


namespace Vito.Transverse.Identity.Domain.ModelsDTO;

public class CultureTranslationDTO
{
    public string ApplicationFk { get; set; } = string.Empty;
    public string CultureFk { get; set; } = string.Empty;
    public string TranslationKey { get; set; }=string.Empty;
    public string TranslationValue { get; set; } = string.Empty;
}


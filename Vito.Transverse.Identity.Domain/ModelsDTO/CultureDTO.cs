namespace Vito.Transverse.Identity.Domain.ModelsDTO;

public partial class CultureDTO
{
    public string Id { get; set; }=string.Empty;
    public string NameTranslationKey { get; set; } = string.Empty;
    public string? CountryFk { get; set; }

    public string? LanguageFk { get; set; }


    public bool IsEnabled { get; set; }


    public string Name { get; set; } = string.Empty;

}

namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public partial class CountryDTO
{
    public string Id { get; set; } = string.Empty;
    
    public string NameTranslationKey { get; set; } = string.Empty;
    
    public int? UtcHoursDifference { get; set; }
}




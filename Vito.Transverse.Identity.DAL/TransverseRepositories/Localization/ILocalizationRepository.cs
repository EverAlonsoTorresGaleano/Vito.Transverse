using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.DAL.TransverseRepositories.Localization;

public interface ILocalizationRepository
{
    
    Task<List<CultureTranslationDTO>> GetAllLocalizedMessagesByCultureIdAsync(string cultureId);
    Task<bool> AddAsync(CultureTranslationDTO newRecordDTO);
    Task<bool> UpdateAsync(CultureTranslationDTO newRecordDTO);
    Task<bool> DeleteAsync(string locationMessageKey);
}

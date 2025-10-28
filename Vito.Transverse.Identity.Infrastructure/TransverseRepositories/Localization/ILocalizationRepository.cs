using System.Linq.Expressions;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using  Vito.Transverse.Identity.Infrastructure.Models;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Localization;

public interface ILocalizationRepository
{

    Task<List<CultureTranslationDTO>> GetLocalizedMessagesListAsync(Expression<Func<CultureTranslation, bool>> filters, DataBaseServiceContext? context = null);
    Task<bool> AddNewCultureTranslationAsync(CultureTranslationDTO newRecordDTO, DataBaseServiceContext? context = null);
    Task<bool> UpdateCultureTranslationAsync(CultureTranslationDTO newRecordDTO, DataBaseServiceContext? context = null);
    Task<bool> DeleteCultureTranslationAsync(string locationMessageKey, DataBaseServiceContext? context = null);
}

using System.Linq.Expressions;
using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.Domain.Models;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.DAL.TransverseRepositories.Localization;

public interface ILocalizationRepository
{

    Task<List<CultureTranslationDTO>> GetLocalizedMessagesListAsync(Expression<Func<CultureTranslation, bool>> filters, DataBaseServiceContext? context = null);
    Task<bool> AddNewCultureTranslationAsync(CultureTranslationDTO newRecordDTO, DataBaseServiceContext? context = null);
    Task<bool> UpdateCultureTranslationAsync(CultureTranslationDTO newRecordDTO, DataBaseServiceContext? context = null);
    Task<bool> DeleteCultureTranslationAsync(string locationMessageKey, DataBaseServiceContext? context = null);
}

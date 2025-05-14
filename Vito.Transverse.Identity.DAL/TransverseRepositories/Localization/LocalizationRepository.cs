using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;
using System.Globalization;
using System.Text;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.DAL.DataBaseContextFactory;
using Vito.Transverse.Identity.Domain.Extensions;
using Vito.Transverse.Identity.Domain.Models;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.DAL.TransverseRepositories.Localization;

public class LocalizationRepository(IDataBaseContextFactory _dataBaseContextFactory, IOptions<CultureSettingsOptions> _cultureSettingsOptions, ILogger<LocalizationRepository> _logger) : ILocalizationRepository
{
    CultureSettingsOptions _cultureSettingsOptionsValues => _cultureSettingsOptions.Value;

    public async Task<List<CultureTranslationDTO>> GetAllLocalizedMessagesByApplicationAsync(long applicationId)
    {
        List<CultureTranslation> returnList;
        DataBaseServiceContext context = default!;
        try
        {
            context = _dataBaseContextFactory.CreateDbContext();
            returnList = await context.CultureTranslations
                .Include(x => x.ApplicationFkNavigation)
                .Include(x => x.CultureFkNavigation.LanguageFkNavigation)
                .Where(x => x.ApplicationFk == applicationId).ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, nameof(GetAllLocalizedMessagesByApplicationAsync));
            throw;
        }

        var returnListDTO = returnList.ToCultureTranslationDTOList();
        return returnListDTO;
    }

    public async Task<bool> AddAsync(CultureTranslationDTO newRecordDTO)
    {
        var saveSuccessfuly = false;
        DataBaseServiceContext context = default!;
        try
        {
            context = _dataBaseContextFactory.CreateDbContext();
            var newRecord = newRecordDTO.ToCultureTranslation();
            await context.CultureTranslations.AddAsync(newRecord);
            var recordAffected = await context.SaveChangesAsync();
            saveSuccessfuly = recordAffected > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(AddAsync));
            throw;
        }

        return saveSuccessfuly;
    }

    public async Task<bool> UpdateAsync(CultureTranslationDTO newRecordDTO)
    {
        var saveSuccessfuly = false;
        DataBaseServiceContext context = default!;
        try
        {
            context = _dataBaseContextFactory.CreateDbContext();
            //Get Culture fron Main Thread
            var cultureId = CultureInfo.CurrentCulture.Name;
            var newRecord = newRecordDTO.ToCultureTranslation();
            var recordToUpdate = await context.CultureTranslations.FirstOrDefaultAsync(x => x.CultureFk.Equals(cultureId) && x.TranslationKey.Equals(newRecord.TranslationKey));
            context.CultureTranslations.Update(newRecord);
            var recordAffected = await context.SaveChangesAsync();

            saveSuccessfuly = recordAffected > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(UpdateAsync));
            throw;
        }

        return saveSuccessfuly;
    }

    public async Task<bool> DeleteAsync(string locationMessageKey)
    {
        var saveSuccessfuly = false;
        DataBaseServiceContext context = default!;
        try
        {
            context = _dataBaseContextFactory.CreateDbContext();
            //Get Culture fron Main Thread
            var cultureId = CultureInfo.CurrentCulture.Name;
            var recordToDelete = await context.CultureTranslations.FirstOrDefaultAsync(x => x.CultureFk.Equals(cultureId) && x.TranslationKey.Equals(locationMessageKey));
            context.CultureTranslations.Remove(recordToDelete!);
            var recordAffected = await context.SaveChangesAsync();

            saveSuccessfuly = recordAffected > 0;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(DeleteAsync));
            throw;
        }

        return saveSuccessfuly;
    }
}

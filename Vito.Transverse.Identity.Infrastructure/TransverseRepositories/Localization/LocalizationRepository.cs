using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Data;
using System.Globalization;
using System.Linq.Expressions;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using Vito.Transverse.Identity.Infrastructure.DataBaseContextFactory;
using Vito.Transverse.Identity.Infrastructure.Extensions;
using Vito.Transverse.Identity.Infrastructure.Models;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Localization;

public class LocalizationRepository(IDataBaseContextFactory dataBaseContextFactory, IOptions<CultureSettingsOptions> cultureSettingsOptions, ILogger<LocalizationRepository> logger) : ILocalizationRepository
{
    CultureSettingsOptions _cultureSettingsOptionsValues => cultureSettingsOptions.Value;

    public async Task<List<CultureTranslationDTO>> GetLocalizedMessagesListAsync(Expression<Func<CultureTranslation, bool>> filters, DataBaseServiceContext? context = null)
    {
        List<CultureTranslation> returnList;

        try
        {
            context = dataBaseContextFactory.CreateDbContext();
            returnList = await context.CultureTranslations.AsNoTracking()
                .Include(x => x.ApplicationFkNavigation)
                .Include(x => x.CultureFkNavigation.LanguageFkNavigation)
                .Where(filters).ToListAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, nameof(GetLocalizedMessagesListAsync));
            throw;
        }

        var returnListDTO = returnList.Select(x => x.ToCultureTranslationDTO()).ToList();
        return returnListDTO;
    }

    public async Task<bool> AddNewCultureTranslationAsync(CultureTranslationDTO newRecordDTO, DataBaseServiceContext? context = null)
    {
        var saveSuccessfuly = false;

        try
        {
            context = dataBaseContextFactory.CreateDbContext();
            var newRecord = newRecordDTO.ToCultureTranslation();
            await context.CultureTranslations.AddAsync(newRecord);
            var recordAffected = await context.SaveChangesAsync();
            saveSuccessfuly = recordAffected > 0;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(AddNewCultureTranslationAsync));
            throw;
        }

        return saveSuccessfuly;
    }

    public async Task<bool> UpsertCultureTranslationAsync(CultureTranslationDTO newRecordDTO, DataBaseServiceContext? context = null)
    {
        var saveSuccessfuly = false;

        try
        {
            context = dataBaseContextFactory.CreateDbContext();
            var newRecord = newRecordDTO.ToCultureTranslation();
            var recordDb = await context.CultureTranslations.FirstOrDefaultAsync(x =>
                x.ApplicationFk == newRecord.ApplicationFk &&
                x.CultureFk.Equals(newRecord.CultureFk) &&
                x.TranslationKey.Equals(newRecord.TranslationKey));
            if (recordDb is null)
            {
                await context.CultureTranslations.AddAsync(newRecord);
            }
            else
            {
                context.Entry(recordDb).CurrentValues.SetValues(newRecord);
            }
            var recordAffected = await context.SaveChangesAsync();

            saveSuccessfuly = recordAffected > 0;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpsertCultureTranslationAsync));
            throw;
        }

        return saveSuccessfuly;
    }

    public async Task<bool> DeleteCultureTranslationAsync(string locationMessageKey, DataBaseServiceContext? context = null)
    {
        var saveSuccessfuly = false;

        try
        {
            context = dataBaseContextFactory.CreateDbContext();
            //Get Culture fron Main Thread
            var cultureId = CultureInfo.CurrentCulture.Name;
            var recordToDelete = await context.CultureTranslations.FirstOrDefaultAsync(x => x.CultureFk.Equals(cultureId) && x.TranslationKey.Equals(locationMessageKey));
            context.CultureTranslations.Remove(recordToDelete!);
            var recordAffected = await context.SaveChangesAsync();

            saveSuccessfuly = recordAffected > 0;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteCultureTranslationAsync));
            throw;
        }

        return saveSuccessfuly;
    }
}

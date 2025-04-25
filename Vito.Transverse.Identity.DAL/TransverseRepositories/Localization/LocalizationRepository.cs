using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Globalization;
using System.Text;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.DAL.DataBaseContextFactory;
using Vito.Transverse.Identity.Domain.Models;
using Vito.Transverse.Identity.Domain.ModelsDTO;
using Vito.Transverse.Identity.Domain.Extensions;

namespace Vito.Transverse.Identity.DAL.TransverseRepositories.Localization;

public class LocalizationRepository(IDataBaseContextFactory _dataBaseContextFactory, IOptions<CultureSettingsOptions> _cultureSettingsOptions, ILogger<LocalizationRepository> _logger) : ILocalizationRepository
{
    CultureSettingsOptions _cultureSettingsOptionsValues => _cultureSettingsOptions.Value;

   

    public async Task<List<CultureTranslationDTO>> GetAllLocalizedMessagesByCultureIdAsync(string cultureId)
    {
        List<CultureTranslation> returnList;
        DataBaseServiceContext context = default!;
        try
        {
            context = _dataBaseContextFactory.CreateDbContext();
            returnList = await context.CultureTranslations.Where(x => x.CultureFk.Equals(cultureId)).ToListAsync();
#if DEBUG
            var fileName = string.Format(_cultureSettingsOptionsValues.LocalizationJsonFilePath!, cultureId);
            var localizationFile = new StringBuilder("{");
            returnList.ForEach(localizationRow =>
            {
                localizationFile.Append($"{(char)34}{localizationRow.TranslationKey}{(char)34}:{(char)34}{localizationRow.TranslationValue}{(char)34},");
            });
            localizationFile.Remove(localizationFile.Length - 1, 1);
            localizationFile.Append("}");

            File.Delete(fileName);
            File.WriteAllText(fileName, localizationFile.ToString());
#endif
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, message: nameof(GetAllLocalizedMessagesByCultureIdAsync));
            returnList = [];// MockDataPopulateEntity.CreateMockDataLocalization().Where(x => x.CultureFk.Equals(cultureId)).ToList();
        }
        finally
        {
            context.Dispose();
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
        }
        finally
        {
            context.Dispose();
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
        }
        finally
        {
            context.Dispose();
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
        }
        finally
        {
            context.Dispose();
        }
        return saveSuccessfuly;
    }
}

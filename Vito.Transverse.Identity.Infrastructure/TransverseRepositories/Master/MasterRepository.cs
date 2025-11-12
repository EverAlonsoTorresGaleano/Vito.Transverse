using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using Vito.Framework.Common.DTO;
using Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using Vito.Transverse.Identity.Infrastructure.DataBaseContextFactory;
using Vito.Transverse.Identity.Infrastructure.Extensions;
using Vito.Transverse.Identity.Infrastructure.Models;
using Vito.Transverse.Identity.Entities.ModelsDTO;
using Vito.Framework.Common.Models.SocialNetworks;

namespace Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Master;

public class MasterRepository(ILogger<MasterRepository> logger, IDataBaseContextFactory dataBaseContextFactory) : IMasterRepository
{
    public async Task<List<SecuencesDTO>> GetAllSecuencesListAsync(Expression<Func<Sequence, bool>> filters, DataBaseServiceContext? context = null)
    {
        List<SecuencesDTO> secuencesDTOList = new();

        try
        {
            context = dataBaseContextFactory.CreateDbContext();
            var sequencesList = await context.Sequences
                .Include(x => x.CompanyFkNavigation)
                .Include(x => x.ApplicationFkNavigation)
                .Where(filters)
                .ToListAsync();

            // Get SequenceType names from GeneralTypeItems
            var sequenceTypeIds = sequencesList.Select(x => x.SequenceTypeFk).Distinct().ToList();
            var generalTypeItems = await context.GeneralTypeItems
                .Where(x => sequenceTypeIds.Contains(x.Id))
                .ToListAsync();

            var generalTypeItemsDict = generalTypeItems.ToDictionary(x => x.Id, x => x.NameTranslationKey);

            secuencesDTOList = sequencesList.Select(seq =>
            {
                var dto = seq.ToSecuencesDTO();
                dto.SequenceTypeNameTranslationKey = generalTypeItemsDict.GetValueOrDefault(seq.SequenceTypeFk, string.Empty);
                return dto;
            }).ToList().OrderBy(x => x.Id).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetAllSecuencesListAsync));
            throw;
        }

        return secuencesDTOList;
    }

    public async Task<SecuencesDTO?> GetSecuenceByIdAsync(long secuenceId, DataBaseServiceContext? context = null)
    {
        SecuencesDTO? secuenceDTO = null;
        try
        {
            context = dataBaseContextFactory.CreateDbContext();
            var sequence = await context.Sequences
                .Include(x => x.CompanyFkNavigation)
                .Include(x => x.ApplicationFkNavigation)
                .FirstOrDefaultAsync(x => x.Id == secuenceId);

            if (sequence != null)
            {
                secuenceDTO = sequence.ToSecuencesDTO();
                
                // Get SequenceType name from GeneralTypeItem
                var generalTypeItem = await context.GeneralTypeItems
                    .FirstOrDefaultAsync(x => x.Id == sequence.SequenceTypeFk);
                
                secuenceDTO.SequenceTypeNameTranslationKey = generalTypeItem?.NameTranslationKey ?? string.Empty;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetSecuenceByIdAsync));
            throw;
        }

        return secuenceDTO;
    }

    public async Task<SecuencesDTO?> CreateNewSecuenceAsync(SecuencesDTO secuenceDTO, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        SecuencesDTO? savedRecord = null;
        var newRecordDb = secuenceDTO.ToSequence();

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            context.Sequences.Add(newRecordDb);
            await context.SaveChangesAsync();
            
            // Reload with includes to get navigation properties
            newRecordDb = await context.Sequences
                .Include(x => x.CompanyFkNavigation)
                .Include(x => x.ApplicationFkNavigation)
                .FirstOrDefaultAsync(x => x.Id == newRecordDb.Id);

            if (newRecordDb != null)
            {
                savedRecord = newRecordDb.ToSecuencesDTO();
                
                // Get SequenceType name from GeneralTypeItem
                var generalTypeItem = await context.GeneralTypeItems
                    .FirstOrDefaultAsync(x => x.Id == newRecordDb.SequenceTypeFk);
                
                savedRecord.SequenceTypeNameTranslationKey = generalTypeItem?.NameTranslationKey ?? string.Empty;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewSecuenceAsync));
            throw;
        }
        return savedRecord;
    }

    public async Task<SecuencesDTO?> UpdateSecuenceByIdAsync(SecuencesDTO secuenceDTO, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        SecuencesDTO? savedRecord = null;

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var sequenceToUpdate = await context.Sequences.FirstOrDefaultAsync(x => x.Id == secuenceDTO.Id);
            if (sequenceToUpdate is null)
            {
                return null;
            }

            var updatedSequence = secuenceDTO.ToSequence();
            context.Entry(sequenceToUpdate).CurrentValues.SetValues(updatedSequence);
            await context.SaveChangesAsync();
            
            // Reload with includes to get navigation properties
            sequenceToUpdate = await context.Sequences
                .Include(x => x.CompanyFkNavigation)
                .Include(x => x.ApplicationFkNavigation)
                .FirstOrDefaultAsync(x => x.Id == secuenceDTO.Id);
            
            if (sequenceToUpdate != null)
            {
                savedRecord = sequenceToUpdate.ToSecuencesDTO();
                
                // Get SequenceType name from GeneralTypeItem
                var generalTypeItem = await context.GeneralTypeItems
                    .FirstOrDefaultAsync(x => x.Id == sequenceToUpdate.SequenceTypeFk);
                
                savedRecord.SequenceTypeNameTranslationKey = generalTypeItem?.NameTranslationKey ?? string.Empty;
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateSecuenceByIdAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<bool> DeleteSecuenceByIdAsync(long secuenceId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        bool deleted = false;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var sequence = await context.Sequences.FirstOrDefaultAsync(x => x.Id == secuenceId);
            if (sequence is null)
            {
                return false;
            }

            context.Sequences.Remove(sequence);
            await context.SaveChangesAsync();
            deleted = true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteSecuenceByIdAsync));
            throw;
        }

        return deleted;
    }

    // Culture CRUD
    public async Task<List<CultureDTO>> GetAllCultureListAsync(DataBaseServiceContext? context = null)
    {
        List<CultureDTO> returnListDTO = new();
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var returnList = await context.Cultures
                .Include(x => x.CountryFkNavigation)
                .Include(x => x.LanguageFkNavigation)
                .ToListAsync();
            returnListDTO = returnList.Select(x => x.ToCultureDTO()).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetAllCultureListAsync));
            throw;
        }
        return returnListDTO;
    }

    public async Task<CultureDTO?> GetCultureByIdAsync(string cultureId, DataBaseServiceContext? context = null)
    {
        CultureDTO? cultureDTO = null;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var culture = await context.Cultures
                .Include(x => x.CountryFkNavigation)
                .Include(x => x.LanguageFkNavigation)
                .FirstOrDefaultAsync(x => x.Id == cultureId);
            cultureDTO = culture?.ToCultureDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetCultureByIdAsync));
            throw;
        }
        return cultureDTO;
    }

    public async Task<CultureDTO?> CreateNewCultureAsync(CultureDTO cultureInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        CultureDTO? savedRecord = null;
        var newRecordDb = cultureInfo.ToCulture();

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            context.Cultures.Add(newRecordDb);
            await context.SaveChangesAsync();
            savedRecord = newRecordDb.ToCultureDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewCultureAsync));
            throw;
        }
        return savedRecord;
    }

    public async Task<CultureDTO?> UpdateCultureByIdAsync(string cultureId, CultureDTO cultureInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        CultureDTO? savedRecord = null;

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var cultureToUpdate = await context.Cultures.FirstOrDefaultAsync(x => x.Id == cultureId);
            if (cultureToUpdate is null)
            {
                return null;
            }

            var updatedCulture = cultureInfo.ToCulture();
            context.Entry(cultureToUpdate).CurrentValues.SetValues(updatedCulture);
            await context.SaveChangesAsync();
            
            // Reload with includes to get navigation properties
            cultureToUpdate = await context.Cultures
                .Include(x => x.CountryFkNavigation)
                .Include(x => x.LanguageFkNavigation)
                .FirstOrDefaultAsync(x => x.Id == cultureId);
            
            savedRecord = cultureToUpdate?.ToCultureDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateCultureByIdAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<bool> DeleteCultureByIdAsync(string cultureId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        bool deleted = false;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var culture = await context.Cultures.FirstOrDefaultAsync(x => x.Id == cultureId);
            if (culture is null)
            {
                return false;
            }

            context.Cultures.Remove(culture);
            await context.SaveChangesAsync();
            deleted = true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteCultureByIdAsync));
            throw;
        }

        return deleted;
    }

    // Language CRUD
    public async Task<List<LanguageDTO>> GetAllLanguageListAsync(DataBaseServiceContext? context = null)
    {
        List<LanguageDTO> returnListDTO = new();
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var returnList = await context.Languages.ToListAsync();
            returnListDTO = returnList.Select(x => x.ToLanguageDTO()).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetAllLanguageListAsync));
            throw;
        }
        return returnListDTO;
    }

    public async Task<LanguageDTO?> GetLanguageByIdAsync(string languageId, DataBaseServiceContext? context = null)
    {
        LanguageDTO? languageDTO = null;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var language = await context.Languages.FirstOrDefaultAsync(x => x.Id == languageId);
            languageDTO = language?.ToLanguageDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetLanguageByIdAsync));
            throw;
        }
        return languageDTO;
    }

    public async Task<LanguageDTO?> CreateNewLanguageAsync(LanguageDTO languageInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        LanguageDTO? savedRecord = null;
        var newRecordDb = languageInfo.ToLanguage();

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            context.Languages.Add(newRecordDb);
            await context.SaveChangesAsync();
            savedRecord = newRecordDb.ToLanguageDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewLanguageAsync));
            throw;
        }
        return savedRecord;
    }

    public async Task<LanguageDTO?> UpdateLanguageByIdAsync(string languageId, LanguageDTO languageInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        LanguageDTO? savedRecord = null;

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var languageToUpdate = await context.Languages.FirstOrDefaultAsync(x => x.Id == languageId);
            if (languageToUpdate is null)
            {
                return null;
            }

            var updatedLanguage = languageInfo.ToLanguage();
            context.Entry(languageToUpdate).CurrentValues.SetValues(updatedLanguage);
            await context.SaveChangesAsync();
            
            // Reload to get updated values
            languageToUpdate = await context.Languages.FirstOrDefaultAsync(x => x.Id == languageId);
            
            savedRecord = languageToUpdate?.ToLanguageDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateLanguageByIdAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<bool> DeleteLanguageByIdAsync(string languageId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        bool deleted = false;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var language = await context.Languages.FirstOrDefaultAsync(x => x.Id == languageId);
            if (language is null)
            {
                return false;
            }

            context.Languages.Remove(language);
            await context.SaveChangesAsync();
            deleted = true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteLanguageByIdAsync));
            throw;
        }

        return deleted;
    }

    // Country CRUD
    public async Task<List<CountryDTO>> GetAllCountryListAsync(DataBaseServiceContext? context = null)
    {
        List<CountryDTO> returnListDTO = new();
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var returnList = await context.Countries.ToListAsync();
            returnListDTO = returnList.Select(x => x.ToCountryDTO()).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetAllCountryListAsync));
            throw;
        }
        return returnListDTO;
    }

    public async Task<CountryDTO?> GetCountryByIdAsync(string countryId, DataBaseServiceContext? context = null)
    {
        CountryDTO? countryDTO = null;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var country = await context.Countries.FirstOrDefaultAsync(x => x.Id == countryId);
            countryDTO = country?.ToCountryDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetCountryByIdAsync));
            throw;
        }
        return countryDTO;
    }

    public async Task<CountryDTO?> CreateNewCountryAsync(CountryDTO countryInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        CountryDTO? savedRecord = null;
        var newRecordDb = countryInfo.ToCountry();

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            context.Countries.Add(newRecordDb);
            await context.SaveChangesAsync();
            savedRecord = newRecordDb.ToCountryDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewCountryAsync));
            throw;
        }
        return savedRecord;
    }

    public async Task<CountryDTO?> UpdateCountryByIdAsync(string countryId, CountryDTO countryInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        CountryDTO? savedRecord = null;

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var countryToUpdate = await context.Countries.FirstOrDefaultAsync(x => x.Id == countryId);
            if (countryToUpdate is null)
            {
                return null;
            }

            var updatedCountry = countryInfo.ToCountry();
            context.Entry(countryToUpdate).CurrentValues.SetValues(updatedCountry);
            await context.SaveChangesAsync();
            
            // Reload to get updated values
            countryToUpdate = await context.Countries.FirstOrDefaultAsync(x => x.Id == countryId);
            
            savedRecord = countryToUpdate?.ToCountryDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateCountryByIdAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<bool> DeleteCountryByIdAsync(string countryId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        bool deleted = false;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var country = await context.Countries.FirstOrDefaultAsync(x => x.Id == countryId);
            if (country is null)
            {
                return false;
            }

            context.Countries.Remove(country);
            await context.SaveChangesAsync();
            deleted = true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteCountryByIdAsync));
            throw;
        }

        return deleted;
    }

    // GeneralTypeItem CRUD
    public async Task<List<GeneralTypeItemDTO>> GetAllGeneralTypeItemListAsync(Expression<Func<GeneralTypeItem, bool>> filters, DataBaseServiceContext? context = null)
    {
        List<GeneralTypeItemDTO> returnListDTO = new();
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var returnList = await context.GeneralTypeItems
                .Include(x => x.ListItemGroupFkNavigation)
                .Where(filters)
                .ToListAsync();
            returnListDTO = returnList.Select(x => x.ToGeneralTypeItemDTO()).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetAllGeneralTypeItemListAsync));
            throw;
        }
        return returnListDTO;
    }


    public async Task<GeneralTypeItemDTO?> GetGeneralTypeItemByIdAsync(long generalTypeItemId, DataBaseServiceContext? context = null)
    {
        GeneralTypeItemDTO? generalTypeItemDTO = null;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var generalTypeItem = await context.GeneralTypeItems
                .Include(x => x.ListItemGroupFkNavigation)
                .FirstOrDefaultAsync(x => x.Id == generalTypeItemId);
            generalTypeItemDTO = generalTypeItem?.ToGeneralTypeItemDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetGeneralTypeItemByIdAsync));
            throw;
        }
        return generalTypeItemDTO;
    }

    public async Task<GeneralTypeItemDTO?> CreateNewGeneralTypeItemAsync(GeneralTypeItemDTO generalTypeItemInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        GeneralTypeItemDTO? savedRecord = null;
        var newRecordDb = generalTypeItemInfo.ToGeneralTypeItem();

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            context.GeneralTypeItems.Add(newRecordDb);
            await context.SaveChangesAsync();
            savedRecord = newRecordDb.ToGeneralTypeItemDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewGeneralTypeItemAsync));
            throw;
        }
        return savedRecord;
    }

    public async Task<GeneralTypeItemDTO?> UpdateGeneralTypeItemByIdAsync(GeneralTypeItemDTO generalTypeItemInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        GeneralTypeItemDTO? savedRecord = null;

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var generalTypeItemToUpdate = await context.GeneralTypeItems.FirstOrDefaultAsync(x => x.Id == generalTypeItemInfo.Id);
            if (generalTypeItemToUpdate is null)
            {
                return null;
            }

            var updatedGeneralTypeItem = generalTypeItemInfo.ToGeneralTypeItem();
            context.Entry(generalTypeItemToUpdate).CurrentValues.SetValues(updatedGeneralTypeItem);
            await context.SaveChangesAsync();
            
            // Reload with includes to get navigation properties
            generalTypeItemToUpdate = await context.GeneralTypeItems
                .Include(x => x.ListItemGroupFkNavigation)
                .FirstOrDefaultAsync(x => x.Id == generalTypeItemInfo.Id);
            
            savedRecord = generalTypeItemToUpdate?.ToGeneralTypeItemDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateGeneralTypeItemByIdAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<bool> DeleteGeneralTypeItemByIdAsync(long generalTypeItemId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        bool deleted = false;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var generalTypeItem = await context.GeneralTypeItems.FirstOrDefaultAsync(x => x.Id == generalTypeItemId);
            if (generalTypeItem is null)
            {
                return false;
            }

            context.GeneralTypeItems.Remove(generalTypeItem);
            await context.SaveChangesAsync();
            deleted = true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteGeneralTypeItemByIdAsync));
            throw;
        }

        return deleted;
    }

    // GeneralTypeGroup CRUD
    public async Task<List<GeneralTypeGroupDTO>> GetAllGeneralTypeGroupListAsync(Expression<Func<GeneralTypeGroup, bool>> filters, DataBaseServiceContext? context = null)
    {
        List<GeneralTypeGroupDTO> returnListDTO = new();
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var returnList = await context.GeneralTypeGroups
                .Where(filters)
                .ToListAsync();
            returnListDTO = returnList.Select(x => x.ToGeneralTypeGroupDTO()).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetAllGeneralTypeGroupListAsync));
            throw;
        }
        return returnListDTO;
    }

    public async Task<GeneralTypeGroupDTO?> GetGeneralTypeGroupByIdAsync(long generalTypeGroupId, DataBaseServiceContext? context = null)
    {
        GeneralTypeGroupDTO? generalTypeGroupDTO = null;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var generalTypeGroup = await context.GeneralTypeGroups.FirstOrDefaultAsync(x => x.Id == generalTypeGroupId);
            generalTypeGroupDTO = generalTypeGroup?.ToGeneralTypeGroupDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetGeneralTypeGroupByIdAsync));
            throw;
        }
        return generalTypeGroupDTO;
    }

    public async Task<GeneralTypeGroupDTO?> CreateNewGeneralTypeGroupAsync(GeneralTypeGroupDTO generalTypeGroupInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        GeneralTypeGroupDTO? savedRecord = null;
        var newRecordDb = generalTypeGroupInfo.ToGeneralTypeGroup();

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            context.GeneralTypeGroups.Add(newRecordDb);
            await context.SaveChangesAsync();
            savedRecord = newRecordDb.ToGeneralTypeGroupDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewGeneralTypeGroupAsync));
            throw;
        }
        return savedRecord;
    }

    public async Task<GeneralTypeGroupDTO?> UpdateGeneralTypeGroupByIdAsync(GeneralTypeGroupDTO generalTypeGroupInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        GeneralTypeGroupDTO? savedRecord = null;

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var generalTypeGroupToUpdate = await context.GeneralTypeGroups.FirstOrDefaultAsync(x => x.Id == generalTypeGroupInfo.Id);
            if (generalTypeGroupToUpdate is null)
            {
                return null;
            }

            var updatedGeneralTypeGroup = generalTypeGroupInfo.ToGeneralTypeGroup();
            context.Entry(generalTypeGroupToUpdate).CurrentValues.SetValues(updatedGeneralTypeGroup);
            await context.SaveChangesAsync();
            
            // Reload to get updated values
            generalTypeGroupToUpdate = await context.GeneralTypeGroups.FirstOrDefaultAsync(x => x.Id == generalTypeGroupInfo.Id);
            
            savedRecord = generalTypeGroupToUpdate?.ToGeneralTypeGroupDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateGeneralTypeGroupByIdAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<bool> DeleteGeneralTypeGroupByIdAsync(long generalTypeGroupId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        bool deleted = false;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var generalTypeGroup = await context.GeneralTypeGroups.FirstOrDefaultAsync(x => x.Id == generalTypeGroupId);
            if (generalTypeGroup is null)
            {
                return false;
            }

            context.GeneralTypeGroups.Remove(generalTypeGroup);
            await context.SaveChangesAsync();
            deleted = true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteGeneralTypeGroupByIdAsync));
            throw;
        }

        return deleted;
    }

    // NotificationTemplate CRUD
    public async Task<List<NotificationTemplateDTO>> GetAllNotificationTemplateListAsync(Expression<Func<NotificationTemplate, bool>> filters, DataBaseServiceContext? context = null)
    {
        List<NotificationTemplateDTO> returnListDTO = new();
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var returnList = await context.NotificationTemplates
                .Include(x => x.CultureFkNavigation)
                .Where(filters)
                .ToListAsync();
            returnListDTO = returnList.Select(x => x.ToNotificationTemplateDTO()).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetAllNotificationTemplateListAsync));
            throw;
        }
        return returnListDTO;
    }

    public async Task<NotificationTemplateDTO?> GetNotificationTemplateByIdAsync(long notificationTemplateId, DataBaseServiceContext? context = null)
    {
        NotificationTemplateDTO? notificationTemplateDTO = null;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var notificationTemplate = await context.NotificationTemplates
                .Include(x => x.CultureFkNavigation)
                .FirstOrDefaultAsync(x => x.Id == notificationTemplateId);
            notificationTemplateDTO = notificationTemplate?.ToNotificationTemplateDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetNotificationTemplateByIdAsync));
            throw;
        }
        return notificationTemplateDTO;
    }

    public async Task<NotificationTemplateDTO?> CreateNewNotificationTemplateAsync(NotificationTemplateDTO notificationTemplateInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        NotificationTemplateDTO? savedRecord = null;
        var newRecordDb = notificationTemplateInfo.ToNotificationTemplate();

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            context.NotificationTemplates.Add(newRecordDb);
            await context.SaveChangesAsync();
            
            // Reload with includes to get navigation properties
            newRecordDb = await context.NotificationTemplates
                .Include(x => x.CultureFkNavigation)
                .FirstOrDefaultAsync(x => x.Id == newRecordDb.Id);
            
            savedRecord = newRecordDb?.ToNotificationTemplateDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewNotificationTemplateAsync));
            throw;
        }
        return savedRecord;
    }

    public async Task<NotificationTemplateDTO?> UpdateNotificationTemplateByIdAsync(NotificationTemplateDTO notificationTemplateInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        NotificationTemplateDTO? savedRecord = null;

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var notificationTemplateToUpdate = await context.NotificationTemplates.FirstOrDefaultAsync(x => x.Id == notificationTemplateInfo.Id);
            if (notificationTemplateToUpdate is null)
            {
                return null;
            }

            var updatedNotificationTemplate = notificationTemplateInfo.ToNotificationTemplate();
            context.Entry(notificationTemplateToUpdate).CurrentValues.SetValues(updatedNotificationTemplate);
            await context.SaveChangesAsync();
            
            // Reload with includes to get navigation properties
            notificationTemplateToUpdate = await context.NotificationTemplates
                .Include(x => x.CultureFkNavigation)
                .FirstOrDefaultAsync(x => x.Id == notificationTemplateInfo.Id);
            
            savedRecord = notificationTemplateToUpdate?.ToNotificationTemplateDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateNotificationTemplateByIdAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<bool> DeleteNotificationTemplateByIdAsync(long notificationTemplateId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        bool deleted = false;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var notificationTemplate = await context.NotificationTemplates.FirstOrDefaultAsync(x => x.Id == notificationTemplateId);
            if (notificationTemplate is null)
            {
                return false;
            }

            context.NotificationTemplates.Remove(notificationTemplate);
            await context.SaveChangesAsync();
            deleted = true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteNotificationTemplateByIdAsync));
            throw;
        }

        return deleted;
    }


}


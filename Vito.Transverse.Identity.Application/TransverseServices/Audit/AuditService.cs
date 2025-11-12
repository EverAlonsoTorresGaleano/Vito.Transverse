using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Extensions;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Transverse.Identity.Application.TransverseServices.Caching;
using Vito.Transverse.Identity.Application.TransverseServices.Culture;
using Vito.Transverse.Identity.Entities.Enums;
using Vito.Transverse.Identity.Entities.ModelsDTO;
using Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using Vito.Transverse.Identity.Infrastructure.Extensions;
using Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Audit;

namespace Vito.Transverse.Identity.Application.TransverseServices.Audit;

public class AuditService(ILogger<AuditService> logger, IAuditRepository auditRepository, ICultureService cultureService, ICachingServiceMemoryCache cachingService) : IAuditService
{
    public async Task<AuditRecordDTO> DeleteRowAuditAsync(long companyId, long userId, object entity, string entityIndex, DeviceInformationDTO devideInformation, bool forceAudit = false)
    {
        AuditRecordDTO? auditInfo = null!;

        var auditType = EntityAuditTypeEnum.EntityAuditType_DeleteRow;
        CompanyEntityAuditDTO? companyAuditEntityInfo = await GetCompanyEntityAuditInfo(companyId, entity, auditType, forceAudit);

        if (companyAuditEntityInfo is not null)
        {
            var auditInformation = CommonExtensions.GetEntityChangesSummary(entity);
            auditInfo = CreateNewAuditRecordDTO(companyId, userId, auditType, companyAuditEntityInfo, entityIndex, auditInformation, devideInformation);
            var recordAdded = await auditRepository.AddNewAuditRecord(auditInfo!);
        }
        return auditInfo!;
    }

    private async Task<CompanyEntityAuditDTO?> GetCompanyEntityAuditInfo(long companyId, object entity, EntityAuditTypeEnum auditType, bool forceAudit)
    {
        CompanyEntityAuditDTO? companyAuditEntityInfo = null;
        if (!forceAudit)
        {
            companyAuditEntityInfo = await IsCompanyEntityEnableForAuditAsync(companyId, entity, auditType);
        }
        else
        {
            var entityList = await GetEntityListAsync();
            var entityInfo = entityList.FirstOrDefault(x => x.EntityName.Equals(entity.GetType().Name, StringComparison.InvariantCultureIgnoreCase));
            if (entityInfo is null)
            {
                companyAuditEntityInfo = new CompanyEntityAuditDTO() { EntityFk = entityInfo!.Id };
            }
        }
        return companyAuditEntityInfo;
    }

    public async Task<AuditRecordDTO> NewRowAuditAsync(long companyId, long userId, object entity, string entityIndex, DeviceInformationDTO devideInformation, bool forceAudit = false)
    {
        AuditRecordDTO? auditInfo = null;

        var auditType = EntityAuditTypeEnum.EntityAuditType_AddRow;
        CompanyEntityAuditDTO? companyAuditEntityInfo = await GetCompanyEntityAuditInfo(companyId, entity, auditType, forceAudit);
        if (companyAuditEntityInfo is not null)
        {
            var auditInformation = CommonExtensions.GetEntityChangesSummary(entity);
            auditInfo = CreateNewAuditRecordDTO(companyId, userId, auditType, companyAuditEntityInfo, entityIndex, auditInformation, devideInformation);
            var recordAdded = await auditRepository.AddNewAuditRecord(auditInfo!);
        }
        return auditInfo!;
    }

    public async Task<AuditRecordDTO> UpdateRowAuditAsync(long companyId, long userId, object oldEntity, object newEntity, string entityIndex, DeviceInformationDTO devideInformation, bool forceAudit = false)
    {
        AuditRecordDTO? auditInfo = null;

        var auditType = EntityAuditTypeEnum.EntityAuditType_UpdateRow;
        CompanyEntityAuditDTO? companyAuditEntityInfo = await GetCompanyEntityAuditInfo(companyId, oldEntity, auditType, forceAudit);

        if (companyAuditEntityInfo is not null)

        {
            var auditInformation = CommonExtensions.GetEntityChangesSummary(oldEntity, newEntity);
            auditInfo = CreateNewAuditRecordDTO(companyId, userId, auditType, companyAuditEntityInfo, entityIndex, auditInformation, devideInformation);
            var recordAdded = await auditRepository.AddNewAuditRecord(auditInfo!);
        }
        return auditInfo!;
    }

    public async Task<CompanyEntityAuditDTO?> IsCompanyEntityEnableForAuditAsync(long companyId, object entity, EntityAuditTypeEnum auditType)
    {
        try
        {
            var entityName = entity.GetType().Name.Replace("DTO", string.Empty);
            var companyAuditList = await GetCompanyEntityAuditsListAsync(companyId);
            var auditEntity = companyAuditList.FirstOrDefault(x => x.CompanyFk == companyId
                            && x.AuditTypeFk == (long)auditType
                            && x.EntityName.Equals($"{entityName}{"s"}")
                            && x.IsActive == true);
            return auditEntity;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, nameof(IsCompanyEntityEnableForAuditAsync));
            throw;
        }
    }

    public async Task<List<EntityDTO>> GetEntityListAsync()
    {
        try
        {
            var returnList = cachingService.GetCacheDataByKey<List<EntityDTO>>(CacheItemKeysEnum.EntityList.ToString());
            if (returnList == null)
            {
                returnList = await auditRepository.GetEntitiesListAsync(x => x.Id > 0);
                cachingService.SetCacheData(CacheItemKeysEnum.EntityList.ToString(), returnList);
            }
            return returnList;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetEntityListAsync));
            throw;
        }

    }

    public async Task<List<ListItemDTO>> GetEntityListItemAsync()
    {
        var listItem = await GetEntityListAsync();
        var returnList = listItem.Select(x => x.ToListItemDTO()).ToList();
        return returnList;
    }

    public async Task<EntityDTO?> GetEntityByIdAsync(long entityId)
    {
        try
        {
            var returnObject = await auditRepository.GetEntityByIdAsync(entityId);
            return returnObject;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetEntityByIdAsync));
            throw;
        }
    }

    public async Task<EntityDTO?> CreateNewEntityAsync(EntityDTO newRecord, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var returnValue = await auditRepository.CreateNewEntityAsync(newRecord, deviceInformation);
            // Clear cache after creating new entity
            cachingService.DeleteCacheDataByKey(CacheItemKeysEnum.EntityList.ToString());
            return returnValue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewEntityAsync));
            throw;
        }
    }

    public async Task<EntityDTO?> UpdateEntityByIdAsync(long entityId, EntityDTO recordToUpdate, DeviceInformationDTO deviceInformation)
    {
        try
        {
            recordToUpdate.Id = entityId;
            var returnValue = await auditRepository.UpdateEntityByIdAsync(recordToUpdate, deviceInformation);
            // Clear cache after updating entity
            cachingService.DeleteCacheDataByKey(CacheItemKeysEnum.EntityList.ToString());
            return returnValue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateEntityByIdAsync));
            throw;
        }
    }

    public async Task<bool> DeleteEntityByIdAsync(long entityId, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var deleted = await auditRepository.DeleteEntityByIdAsync(entityId, deviceInformation);
            // Clear cache after deleting entity
            if (deleted)
            {
                cachingService.DeleteCacheDataByKey(CacheItemKeysEnum.EntityList.ToString());
            }
            return deleted;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteEntityByIdAsync));
            throw;
        }
    }

    public async Task<List<CompanyEntityAuditDTO>> GetCompanyEntityAuditsListAsync(long? companyId)
    {
        try
        {
            var returnList = cachingService.GetCacheDataByKey<List<CompanyEntityAuditDTO>>(CacheItemKeysEnum.CompanyEntityAuditListByCompanyId + companyId.ToString());
            if (returnList == null)
            {
                returnList = await auditRepository.GetCompanyEntityAuditsListAsync(x =>
                    (x.CompanyFk == companyId || companyId == null));
                cachingService.SetCacheData(CacheItemKeysEnum.CompanyEntityAuditListByCompanyId + companyId.ToString(), returnList);
            }
            return returnList;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetCompanyEntityAuditsListAsync));
            throw;
        }
    }

    public async Task<CompanyEntityAuditDTO?> GetCompanyEntityAuditByIdAsync(long companyEntityAuditId)
    {
        try
        {
            var returnObject = await auditRepository.GetCompanyEntityAuditByIdAsync(companyEntityAuditId);
            return returnObject;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetCompanyEntityAuditByIdAsync));
            throw;
        }
    }

    public async Task<CompanyEntityAuditDTO?> CreateNewCompanyEntityAuditAsync(CompanyEntityAuditDTO newRecord, DeviceInformationDTO deviceInformation)
    {
        try
        {
            var returnValue = await auditRepository.CreateNewCompanyEntityAuditAsync(newRecord, deviceInformation);
            return returnValue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewCompanyEntityAuditAsync));
            throw;
        }
    }

    public async Task<CompanyEntityAuditDTO?> UpdateCompanyEntityAuditByIdAsync(long companyEntityAuditId, CompanyEntityAuditDTO recordToUpdate, DeviceInformationDTO deviceInformation)
    {
        try
        {
            recordToUpdate.Id = companyEntityAuditId;
            return await auditRepository.UpdateCompanyEntityAuditByIdAsync(recordToUpdate, deviceInformation);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateCompanyEntityAuditByIdAsync));
            throw;
        }
    }

    public async Task<bool> DeleteCompanyEntityAuditByIdAsync(long companyEntityAuditId, DeviceInformationDTO deviceInformation)
    {
        try
        {
            return await auditRepository.DeleteCompanyEntityAuditByIdAsync(companyEntityAuditId, deviceInformation);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteCompanyEntityAuditByIdAsync));
            throw;
        }
    }

    public async Task<List<AuditRecordDTO>> GetAuditRecordsListAsync(long? companyId)
    {
        try
        {
            var returnValue = await auditRepository.GetAuditRecordListAsync(x =>
                    (companyId == null || x.CompanyFk == companyId));
            return returnValue;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetAuditRecordsListAsync));
            throw;
        }
    }

    public async Task<List<ActivityLogDTO>> GetActivityLogListAsync(long? companyId)
    {
        try
        {
            var returnList = await auditRepository.GetActivityLogListAsync(x =>
                    (companyId == null || x.CompanyFk == companyId));
            return returnList;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetActivityLogListAsync));
            throw;
        }
    }


    public async Task<ActivityLogDTO?> AddNewActivityLogAsync(long? companyId, long? applicationId, long? userId, long? roleId, DeviceInformationDTO deviceInformation, OAuthActionTypeEnum actionStatus, DataBaseServiceContext? context = null)
    {
        ActivityLogDTO? savedRecord = null;
        try
        {
            //var contextTx = dataBaseContextFactory.GetDbContext(context);
            ActivityLogDTO activityLogDb = new()
            {
                CompanyFk = companyId!.Value,
                UserFk = userId!.Value,
                ActionTypeFk = (int)actionStatus,
                Browser = deviceInformation.Browser!,
                CultureId = deviceInformation.CultureId!,
                DeviceName = deviceInformation.HostName!,
                DeviceType = deviceInformation.DeviceType!,
                Engine = deviceInformation.Engine!,
                EventDate = cultureService.UtcNow().DateTime,
                IpAddress = deviceInformation.IpAddress!,
                Platform = deviceInformation.Platform!,
                EndPointUrl = deviceInformation.EndPointUrl!,
                Method = deviceInformation.Method!,
                QueryString = deviceInformation.QueryString!,
                Referer = deviceInformation.Referer!,
                UserAgent = deviceInformation.UserAgent!,
                ApplicationId = applicationId ?? deviceInformation.ApplicationId!.Value,
                RoleId = roleId ?? deviceInformation.RoleId!.Value,

            };
            savedRecord = await auditRepository.AddNewActivityLogAsync(activityLogDb, null);// contextTx);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(AddNewActivityLogAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<ActivityLogDTO?> AddNewActivityLogAsync(DeviceInformationDTO deviceInformation, OAuthActionTypeEnum actionStatus, DataBaseServiceContext? context = null)
    {
        ActivityLogDTO? returnValue = null!;
        try
        {
            var companyId = deviceInformation.CompanyId;
            var applicationId = deviceInformation.ApplicationId;
            var userId = deviceInformation.UserId;
            var roleId = deviceInformation.RoleId;
            returnValue = await AddNewActivityLogAsync(companyId!.Value, applicationId, userId, roleId, deviceInformation, actionStatus, context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(AddNewActivityLogAsync));
        }
        return returnValue!;
    }


    public async Task<List<NotificationDTO>> GetNotificationsListAsync(long? companyId)
    {
        try
        {
            var returnList = await auditRepository.GetNotificationsListAsync(x =>
                        (companyId == null || x.CompanyFk == companyId));
            return returnList;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetNotificationsListAsync));
            throw;
        }
    }

    public async Task<Dictionary<string, object>> GetDatabaseHealth()
    {
        try
        {
            Dictionary<string, object> returnList = await auditRepository.GetDatabaseHealth();
            return returnList;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetDatabaseHealth));
            throw;
        }
    }

    private AuditRecordDTO? CreateNewAuditRecordDTO(long companyId, long userId, EntityAuditTypeEnum auditType, CompanyEntityAuditDTO companyEntity, string entityIndex, string auditInformation, DeviceInformationDTO devideInformation)
    {
        var cultureId = cultureService.GetCurrentCultureId();
        AuditRecordDTO newRecord = new()
        {
            CreationDate = cultureService.UtcNow().DateTime,
            CompanyFk = companyId,
            UserFk = userId,
            AuditTypeFk = (long)auditType,
            EntityFk = companyEntity.EntityFk,
            AuditEntityIndex = entityIndex,
            CultureFk = cultureId,
            Browser = devideInformation.Browser!,
            DeviceType = devideInformation.DeviceType!,
            Engine = devideInformation.Engine!,
            HostName = devideInformation.HostName!,
            IpAddress = devideInformation.IpAddress!,
            Platform = devideInformation.Platform!,
            EndPointUrl = devideInformation.EndPointUrl!,
            Method = devideInformation.Method!,
            QueryString = devideInformation.QueryString!,
            UserAgent = devideInformation.UserAgent!,
            Referer = devideInformation.Referer!,
            ApplicationId = devideInformation.ApplicationId!.Value,
            RoleId = devideInformation.RoleId!.Value,
            AuditChanges = auditInformation
        };
        return newRecord;
    }


}

using Microsoft.Extensions.Logging;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Extensions;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Transverse.Identity.BAL.TransverseServices.Caching;
using Vito.Transverse.Identity.DAL.TransverseRepositories.Audit;
using Vito.Transverse.Identity.DAL.TransverseRepositories.Culture;
using Vito.Transverse.Identity.Domain.Enums;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.BAL.TransverseServices.Audit;

public class AuditService(IAuditRepository auditRepository, ILogger<AuditService> logger, ICultureRepository cultureRepository, ICachingServiceMemoryCache cachingService) : IAuditService
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
            logger.LogError(ex, message: nameof(GetCompanyEntityAuditsListAsync));
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

    private AuditRecordDTO? CreateNewAuditRecordDTO(long companyId, long userId, EntityAuditTypeEnum auditType, CompanyEntityAuditDTO companyEntity, string entityIndex, string auditInformation, DeviceInformationDTO devideInformation)
    {
        var cultureId = cultureRepository.GetCurrentCultureId();
        AuditRecordDTO newRecord = new()
        {
            CreationDate = cultureRepository.UtcNow().DateTime,
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

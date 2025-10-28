using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Models.SocialNetworks;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace  Vito.Transverse.Identity.Application.TransverseServices.Audit;

public interface IAuditService
{

    Task<AuditRecordDTO> NewRowAuditAsync(long companyId, long userId, object entity, string entityIndex, DeviceInformationDTO devideInformation, bool forceAudit = false);

    Task<AuditRecordDTO> UpdateRowAuditAsync(long companyId, long userId, object oldEntity, object newEntity, string entityIndex, DeviceInformationDTO devideInformation, bool forceAudit = false);

    Task<AuditRecordDTO> DeleteRowAuditAsync(long companyId, long userId, object entity, string entityIndex, DeviceInformationDTO devideInformation, bool forceAudit = false);

    Task<CompanyEntityAuditDTO?> IsCompanyEntityEnableForAuditAsync(long companyId, object entityName, EntityAuditTypeEnum auditType);

    Task<List<EntityDTO>> GetEntityListAsync();

    Task<List<CompanyEntityAuditDTO>> GetCompanyEntityAuditsListAsync(long? companyId);

    Task<List<AuditRecordDTO>> GetAuditRecordsListAsync(long? companyId);

    Task<List<ActivityLogDTO>> GetActivityLogListAsync(long? companyId);

    Task<List<NotificationDTO>> GetNotificationsListAsync(long? companyId);

    Task<Dictionary<string, object>> GetDatabaseHealth();

    Task<ActivityLogDTO?> AddNewActivityLogAsync(long? companyId, long? applicationId, long? userId, long? roleId, DeviceInformationDTO deviceInformation, OAuthActionTypeEnum actionStatus, DataBaseServiceContext? context = null);

    Task<ActivityLogDTO?> AddNewActivityLogAsync(DeviceInformationDTO deviceInformation, OAuthActionTypeEnum actionStatus, DataBaseServiceContext? context = null);


}

using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.BAL.TransverseServices.Audit;

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
}

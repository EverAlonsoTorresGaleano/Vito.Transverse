using System.Linq.Expressions;
using Vito.Framework.Common.Models.SocialNetworks;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using  Vito.Transverse.Identity.Infrastructure.Models;
using Vito.Transverse.Identity.Entities.ModelsDTO;
using Vito.Framework.Common.DTO;

namespace  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Audit;

public interface IAuditRepository
{

    Task<AuditRecordDTO?> AddNewAuditRecord(AuditRecordDTO newRecord, DataBaseServiceContext? context = null);

    Task<List<CompanyEntityAuditDTO>> GetCompanyEntityAuditsListAsync(Expression<Func<CompanyEntityAudit, bool>> filters, DataBaseServiceContext? context = null);

    Task<CompanyEntityAuditDTO?> GetCompanyEntityAuditByIdAsync(long companyEntityAuditId, DataBaseServiceContext? context = null);

    Task<CompanyEntityAuditDTO?> CreateNewCompanyEntityAuditAsync(CompanyEntityAuditDTO newRecord, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<CompanyEntityAuditDTO?> UpdateCompanyEntityAuditByIdAsync(CompanyEntityAuditDTO recordToUpdate, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<bool> DeleteCompanyEntityAuditByIdAsync(long companyEntityAuditId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<List<EntityDTO>> GetEntitiesListAsync(Expression<Func<Entity, bool>> filters, DataBaseServiceContext? context = null);

    Task<EntityDTO?> GetEntityByIdAsync(long entityId, DataBaseServiceContext? context = null);

    Task<EntityDTO?> CreateNewEntityAsync(EntityDTO newRecord, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<EntityDTO?> UpdateEntityByIdAsync(EntityDTO recordToUpdate, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<bool> DeleteEntityByIdAsync(long entityId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<List<AuditRecordDTO>> GetAuditRecordListAsync(Expression<Func<AuditRecord, bool>> filters, DataBaseServiceContext? context = null);

    Task<List<ActivityLogDTO>> GetActivityLogListAsync(Expression<Func<ActivityLog, bool>> filters, DataBaseServiceContext? context = null);

    Task<List<NotificationDTO>> GetNotificationsListAsync(Expression<Func<Notification, bool>> filters, DataBaseServiceContext? context = null);
    Task<Dictionary<string, object>> GetDatabaseHealth();

    Task<ActivityLogDTO?> AddNewActivityLogAsync(ActivityLogDTO newRecord, DataBaseServiceContext? context = null);
}

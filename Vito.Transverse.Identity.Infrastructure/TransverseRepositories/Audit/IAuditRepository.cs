using System.Linq.Expressions;
using Vito.Framework.Common.Models.SocialNetworks;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using  Vito.Transverse.Identity.Infrastructure.Models;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Audit;

public interface IAuditRepository
{

    Task<AuditRecordDTO?> AddNewAuditRecord(AuditRecordDTO newRecord, DataBaseServiceContext? context = null);

    Task<List<CompanyEntityAuditDTO>> GetCompanyEntityAuditsListAsync(Expression<Func<CompanyEntityAudit, bool>> filters, DataBaseServiceContext? context = null);

    Task<List<EntityDTO>> GetEntitiesListAsync(Expression<Func<Entity, bool>> filters, DataBaseServiceContext? context = null);

    Task<List<AuditRecordDTO>> GetAuditRecordListAsync(Expression<Func<AuditRecord, bool>> filters, DataBaseServiceContext? context = null);

    Task<List<ActivityLogDTO>> GetActivityLogListAsync(Expression<Func<ActivityLog, bool>> filters, DataBaseServiceContext? context = null);

    Task<List<NotificationDTO>> GetNotificationsListAsync(Expression<Func<Notification, bool>> filters, DataBaseServiceContext? context = null);
    Task<Dictionary<string, object>> GetDatabaseHealth();

    Task<ActivityLogDTO?> AddNewActivityLogAsync(ActivityLogDTO newRecord, DataBaseServiceContext? context = null);
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using Vito.Framework.Common.Extensions;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.DAL.DataBaseContextFactory;
using Vito.Transverse.Identity.Domain.Extensions;
using Vito.Transverse.Identity.Domain.Models;
using Vito.Transverse.Identity.Domain.ModelsDTO;
using Vito.Transverse.Identity.Domain.Options;

namespace Vito.Transverse.Identity.DAL.TransverseRepositories.Audit;

public class AuditRepository(IDataBaseContextFactory dataBaseContextFactory, IOptions<DataBaseSettingsOptions> dataBaseSettingsOptions, ILogger<AuditRepository> logger) : IAuditRepository
{
    DataBaseSettingsOptions dataBaseSettingsOptionsValue = dataBaseSettingsOptions.Value;
    public async Task<AuditRecordDTO?> AddNewAuditRecord(AuditRecordDTO newRecord, DataBaseServiceContext? context = null)
    {
        AuditRecordDTO? recordSaved = null;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var dbRecord = newRecord.ToAuditRecord();
            context.AuditRecords.Add(dbRecord);
            var recordAffected = await context.SaveChangesAsync();
            recordSaved = dbRecord.ToAuditRecordDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, nameof(AddNewAuditRecord));
            throw;
        }
        return recordSaved;
    }



    public async Task<List<EntityDTO>> GetEntitiesListAsync(Expression<Func<Entity, bool>> filters, DataBaseServiceContext? context = null)
    {
        var returnList = new List<EntityDTO>();
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var bdList = await context.Entities
                .Where(filters).ToListAsync();
            returnList = bdList.ToEntityDTOList().OrderBy(x => x.Id).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetEntitiesListAsync));
            throw;
        }

        return returnList;
    }

    public async Task<List<CompanyEntityAuditDTO>> GetCompanyEntityAuditsListAsync(Expression<Func<CompanyEntityAudit, bool>> filters, DataBaseServiceContext? context = null)
    {
        var returnList = new List<CompanyEntityAuditDTO>();
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var bdList = await context.CompanyEntityAudits
                .Include(x => x.EntityFkNavigation)
                .Include(x => x.AuditTypeFkNavigation)
                .Include(x => x.CompanyFkNavigation)
                .Where(filters).ToListAsync();
            returnList = bdList.ToCompanyEntityAuditDTOList().OrderBy(x => x.CompanyFk).ThenBy(x => x.EntityName).ThenBy(x => x.AuditTypeFk).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetCompanyEntityAuditsListAsync));
            throw;
        }

        return returnList;
    }

    public async Task<List<AuditRecordDTO>> GetAuditRecordListAsync(Expression<Func<AuditRecord, bool>> filters, DataBaseServiceContext? context = null)
    {
        var returnList = new List<AuditRecordDTO>();
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var bdList = await context.AuditRecords
                .Include(x => x.EntityFkNavigation)
                .Include(x => x.AuditTypeFkNavigation)
                .Include(x => x.UserFkNavigation)
                .Include(x => x.CompanyFkNavigation)
                .Where(filters!).ToListAsync();
            returnList = bdList.ToAuditRecordDTOList().OrderByDescending(x => x.CreationDate).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetAuditRecordListAsync));
            throw;
        }

        return returnList;
    }

    public async Task<List<ActivityLogDTO>> GetActivityLogListAsync(Expression<Func<ActivityLog, bool>> filters, DataBaseServiceContext? context = null)
    {
        var returnList = new List<ActivityLogDTO>();
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var bdList = await context.ActivityLogs
                .Include(x => x.UserFkNavigation)
                .ThenInclude(x => x.CompanyFkNavigation)
                .Include(x => x.ActionTypeFkNavigation)
                .Where(filters).ToListAsync();
            returnList = bdList.ToActivityLogDTOList().OrderByDescending(x => x.EventDate).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetActivityLogListAsync));
            throw;
        }

        return returnList;
    }

    public async Task<List<NotificationDTO>> GetNotificationsListAsync(Expression<Func<Notification, bool>> filters, DataBaseServiceContext? context = null)
    {
        var returnList = new List<NotificationDTO>();
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var bdList = await context.Notifications
                .Include(x => x.NotificationTemplate)
                .ThenInclude(x => x.CultureFkNavigation)
                .Include(x => x.CompanyFkNavigation)
                .Include(x => x.NotificationTypeFkNavigation)
                .Where(filters).ToListAsync();
            returnList = bdList.ToNotificationDTOList().OrderByDescending(x => x.CreationDate).ToList(); ;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetNotificationsListAsync));
            throw;
        }

        return returnList;
    }

    public async Task<Dictionary<string, object>> GetDatabaseHealth()
    {
        Dictionary<string, object> healthCheckData = [];
        var returnList = new List<EntityDTO>();
        try
        {
            var transverseDBConnectionString = dataBaseSettingsOptionsValue!.ConnectionStrings!.First(x => x.ConnectionName.Equals(dataBaseContextFactory.DefaultDatabaseId().ToString()));
            healthCheckData.Add(nameof(transverseDBConnectionString.ConnectionType), transverseDBConnectionString.ConnectionType);
            healthCheckData.Add(nameof(transverseDBConnectionString.ConnectionName), transverseDBConnectionString.ConnectionName);
            healthCheckData.Add(nameof(transverseDBConnectionString.ConnectionString), transverseDBConnectionString.ConnectionString);
            healthCheckData.Add(nameof(transverseDBConnectionString.MaxRetryDelay), transverseDBConnectionString.MaxRetryDelay);
            healthCheckData.Add(nameof(transverseDBConnectionString.RetryCount), transverseDBConnectionString.RetryCount);
            healthCheckData.Add(nameof(transverseDBConnectionString.TimeOut), transverseDBConnectionString.TimeOut);
            var context = dataBaseContextFactory.GetDbContext();
            healthCheckData.Add(nameof(dataBaseContextFactory.GetDbContext), true);
            var entityList = await context.Entities.ToListAsync();
            healthCheckData.Add(nameof(context.Entities), true);
            healthCheckData.Add($"{nameof(context.Entities)}{nameof(entityList.Count)}", entityList.Count);
            var endpointList = await context.Endpoints.ToListAsync();
            healthCheckData.Add(nameof(context.Endpoints), true);
            healthCheckData.Add($"{nameof(context.Endpoints)}{nameof(entityList.Count)}", endpointList.Count);
        }
        catch (Exception ex)
        {
            healthCheckData.Add(nameof(Exception), ex.GetErrorStakTrace());
            logger.LogError(ex, message: nameof(GetDatabaseHealth));
        }

        return healthCheckData;
    }
}

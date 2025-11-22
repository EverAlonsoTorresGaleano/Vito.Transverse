using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Extensions;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.Entities.ModelsDTO;
using Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using Vito.Transverse.Identity.Infrastructure.DataBaseContextFactory;
using Vito.Transverse.Identity.Infrastructure.Extensions;
using Vito.Transverse.Identity.Infrastructure.Models;
using Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Culture;

namespace  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Audit;

public class AuditRepository(IDataBaseContextFactory dataBaseContextFactory, IOptions<DataBaseSettingsOptions> dataBaseSettingsOptions,ICultureRepository cultureRepository, ILogger<AuditRepository> logger) : IAuditRepository
{
    readonly DataBaseSettingsOptions dataBaseSettingsOptionsValue = dataBaseSettingsOptions.Value;
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
            var bdList = await context.Entities.AsNoTracking()
                .Where(filters).ToListAsync();
            returnList = bdList.Select(x => x.ToEntityDTO()).ToList().OrderBy(x => x.Id).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetEntitiesListAsync));
            throw;
        }

        return returnList;
    }

    public async Task<EntityDTO?> GetEntityByIdAsync(long entityId, DataBaseServiceContext? context = null)
    {
        EntityDTO? entityDTO = null;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var entity = await context.Entities.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == entityId);
            entityDTO = entity?.ToEntityDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetEntityByIdAsync));
            throw;
        }

        return entityDTO;
    }

    public async Task<EntityDTO?> CreateNewEntityAsync(EntityDTO newRecord, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        EntityDTO? savedRecord = null;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var newRecordDb = newRecord.ToEntity();
            context.Entities.Add(newRecordDb);
            await context.SaveChangesAsync();
            
            savedRecord = newRecordDb.ToEntityDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewEntityAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<EntityDTO?> UpdateEntityByIdAsync(EntityDTO recordToUpdate, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        EntityDTO? savedRecord = null;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var entityToUpdate = await context.Entities.FirstOrDefaultAsync(x => x.Id == recordToUpdate.Id);
            if (entityToUpdate is null)
            {
                return null;
            }

            entityToUpdate.SchemaName = recordToUpdate.SchemaName;
            entityToUpdate.EntityName = recordToUpdate.EntityName;
            entityToUpdate.IsActive = recordToUpdate.IsActive;
            entityToUpdate.IsSystemEntity = recordToUpdate.IsSystemEntity;

            await context.SaveChangesAsync();
            
            savedRecord = entityToUpdate.ToEntityDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateEntityByIdAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<bool> DeleteEntityByIdAsync(long entityId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        bool deleted = false;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var entityToDelete = await context.Entities.FirstOrDefaultAsync(x => x.Id == entityId);
            if (entityToDelete is null)
            {
                return false;
            }

            context.Entities.Remove(entityToDelete);
            var recordsAffected = await context.SaveChangesAsync();
            deleted = recordsAffected > 0;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteEntityByIdAsync));
            throw;
        }

        return deleted;
    }

    public async Task<List<CompanyEntityAuditDTO>> GetCompanyEntityAuditsListAsync(Expression<Func<CompanyEntityAudit, bool>> filters, DataBaseServiceContext? context = null)
    {
        var returnList = new List<CompanyEntityAuditDTO>();
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var bdList = await context.CompanyEntityAudits.AsNoTracking()
                .Include(x => x.EntityFkNavigation)
                .Include(x => x.AuditTypeFkNavigation)
                .Include(x => x.CompanyFkNavigation)
                .Where(filters).ToListAsync();
            returnList = bdList.Select(x => x.ToCompanyEntityAuditDTO()).ToList().OrderBy(x => x.CompanyFk).ThenBy(x => x.EntityName).ThenBy(x => x.AuditTypeFk).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetCompanyEntityAuditsListAsync));
            throw;
        }

        return returnList;
    }

    public async Task<CompanyEntityAuditDTO?> GetCompanyEntityAuditByIdAsync(long companyEntityAuditId, DataBaseServiceContext? context = null)
    {
        CompanyEntityAuditDTO? companyEntityAuditDTO = null;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var companyEntityAudit = await context.CompanyEntityAudits.AsNoTracking()
                .Include(x => x.EntityFkNavigation)
                .Include(x => x.AuditTypeFkNavigation)
                .Include(x => x.CompanyFkNavigation)
                .FirstOrDefaultAsync(x => x.Id == companyEntityAuditId);
            companyEntityAuditDTO = companyEntityAudit?.ToCompanyEntityAuditDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetCompanyEntityAuditByIdAsync));
            throw;
        }

        return companyEntityAuditDTO;
    }

    public async Task<CompanyEntityAuditDTO?> CreateNewCompanyEntityAuditAsync(CompanyEntityAuditDTO newRecord, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        CompanyEntityAuditDTO? savedRecord = null;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var newRecordDb = newRecord.ToCompanyEntityAudit();
            newRecordDb.CreatedDate = cultureRepository.UtcNow().DateTime;
            newRecordDb.CreatedByUserFk = deviceInformation.UserId!;
            context.CompanyEntityAudits.Add(newRecordDb);
            await context.SaveChangesAsync();
            
            // Reload with includes to get navigation properties
            newRecordDb = await context.CompanyEntityAudits
                .Include(x => x.EntityFkNavigation)
                .Include(x => x.AuditTypeFkNavigation)
                .Include(x => x.CompanyFkNavigation)
                .FirstOrDefaultAsync(x => x.Id == newRecordDb.Id);
            
            savedRecord = newRecordDb?.ToCompanyEntityAuditDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewCompanyEntityAuditAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<CompanyEntityAuditDTO?> UpdateCompanyEntityAuditByIdAsync(CompanyEntityAuditDTO recordToUpdate, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        CompanyEntityAuditDTO? savedRecord = null;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var companyEntityAuditToUpdate = await context.CompanyEntityAudits.FirstOrDefaultAsync(x => x.Id == recordToUpdate.Id);
            if (companyEntityAuditToUpdate is null)
            {
                return null;
            }

            companyEntityAuditToUpdate.CompanyFk = recordToUpdate.CompanyFk;
            companyEntityAuditToUpdate.EntityFk = recordToUpdate.EntityFk;
            companyEntityAuditToUpdate.AuditTypeFk = recordToUpdate.AuditTypeFk;
            companyEntityAuditToUpdate.IsActive = recordToUpdate.IsActive;
            companyEntityAuditToUpdate.LastUpdatedDate = cultureRepository.UtcNow().DateTime;
            companyEntityAuditToUpdate.LastUpdatedByUserFk = deviceInformation.UserId;

            await context.SaveChangesAsync();
            
            // Reload with includes to get navigation properties
            companyEntityAuditToUpdate = await context.CompanyEntityAudits
                .Include(x => x.EntityFkNavigation)
                .Include(x => x.AuditTypeFkNavigation)
                .Include(x => x.CompanyFkNavigation)
                .FirstOrDefaultAsync(x => x.Id == recordToUpdate.Id);
            
            savedRecord = companyEntityAuditToUpdate?.ToCompanyEntityAuditDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateCompanyEntityAuditByIdAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<bool> DeleteCompanyEntityAuditByIdAsync(long companyEntityAuditId, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        bool deleted = false;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var companyEntityAuditToDelete = await context.CompanyEntityAudits.FirstOrDefaultAsync(x => x.Id == companyEntityAuditId);
            if (companyEntityAuditToDelete is null)
            {
                return false;
            }

            context.CompanyEntityAudits.Remove(companyEntityAuditToDelete);
            var recordsAffected = await context.SaveChangesAsync();
            deleted = recordsAffected > 0;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteCompanyEntityAuditByIdAsync));
            throw;
        }

        return deleted;
    }

    public async Task<List<AuditRecordDTO>> GetAuditRecordListAsync(Expression<Func<AuditRecord, bool>> filters, DataBaseServiceContext? context = null)
    {
        var returnList = new List<AuditRecordDTO>();
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var bdList = await context.AuditRecords.AsNoTracking()
                .Include(x => x.EntityFkNavigation)
                .Include(x => x.AuditTypeFkNavigation)
                .Include(x => x.UserFkNavigation)
                .Include(x => x.CompanyFkNavigation)
                .Where(filters!).ToListAsync();
            returnList = bdList.Select(x => x.ToAuditRecordDTO()).ToList().OrderByDescending(x => x.CreationDate).ToList();
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
            var bdList = await context.ActivityLogs.AsNoTracking()
                .Include(x => x.UserFkNavigation)
                .ThenInclude(x => x.CompanyFkNavigation)
                .Include(x => x.ActionTypeFkNavigation)
                .Where(filters).ToListAsync();
            returnList = bdList.Select(x => x.ToActivityLogDTO()).ToList().OrderByDescending(x => x.EventDate).ToList();
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
            var bdList = await context.Notifications.AsNoTracking()
                .Include(x => x.NotificationTemplate)
                .ThenInclude(x => x.CultureFkNavigation)
                .Include(x => x.CompanyFkNavigation)
                .Include(x => x.NotificationTypeFkNavigation)
                .Where(filters).ToListAsync();
            returnList = bdList.Select(x => x.ToNotificationDTO()).ToList().OrderByDescending(x => x.CreationDate).ToList(); ;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetNotificationsListAsync));
            throw;
        }

        return returnList;
    }

    public async Task<ActivityLogDTO?> AddNewActivityLogAsync(ActivityLogDTO newRecord, DataBaseServiceContext? context = null)
    {
        ActivityLogDTO? savedRecord = null;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var newRecordDb = newRecord.ToActivityLog();
            context.ActivityLogs.Add(newRecordDb);
            var resultValue = await context.SaveChangesAsync();
            savedRecord = newRecordDb.ToActivityLogDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(AddNewActivityLogAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<Dictionary<string, object>> GetDatabaseHealth()
    {
        Dictionary<string, object> healthCheckData = [];
        var returnList = new List<EntityDTO>();
        try
        {
            var transverseDBConnectionString = dataBaseSettingsOptionsValue!.ConnectionStrings!.First(x => x.ConnectionName.Equals(dataBaseContextFactory.DefaultDatabaseName().ToString()));
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

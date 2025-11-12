using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Linq.Expressions;
using Vito.Framework.Common.DTO;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContextFactory;
using  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Security;
using  Vito.Transverse.Identity.Infrastructure.Extensions;
using  Vito.Transverse.Identity.Infrastructure.Models;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Applications;

public class ApplicationsRepository(ILogger<SecurityRepository> logger, IDataBaseContextFactory dataBaseContextFactory) : IApplicationsRepository
{


    public async Task<ApplicationDTO?> CreateNewApplicationAsync(ApplicationDTO newRecord, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        ApplicationDTO? savedRecord = null;
        var newRecordDb = newRecord.ToApplication();

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            context.Applications.Add(newRecordDb);
            await context.SaveChangesAsync();
            savedRecord = newRecordDb.ToApplicationDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewApplicationAsync));
        }
        return savedRecord;
    }

    public async Task<ApplicationDTO?> UpdateApplicationAsync(ApplicationDTO recordToUpdate, DataBaseServiceContext? context = null)
    {
        ApplicationDTO? savedRecord = null;

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var recordToUpdateDb = await context.Applications.FirstOrDefaultAsync(x => x.Id == recordToUpdate.Id);
            if (recordToUpdateDb is null)
            {
                return null;
            }

            recordToUpdateDb.NameTranslationKey = recordToUpdate.NameTranslationKey;
            recordToUpdateDb.DescriptionTranslationKey = recordToUpdate.DescriptionTranslationKey;
            recordToUpdateDb.Avatar = recordToUpdate.Avatar;
            recordToUpdateDb.IsActive = recordToUpdate.IsActive;
            recordToUpdateDb.LastUpdateByUserFk = recordToUpdate.LastUpdateByUserFk;
            recordToUpdateDb.LastUpdateDate = recordToUpdate.LastUpdateDate;

            await context.SaveChangesAsync();
            savedRecord = recordToUpdateDb.ToApplicationDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateApplicationAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<bool> DeleteApplicationAsync(long applicationId, long updatedByUserId, DateTime actionDate, DataBaseServiceContext? context = null)
    {
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var recordToDelete = await context.Applications.FirstOrDefaultAsync(x => x.Id == applicationId);
            if (recordToDelete is null)
            {
                return false;
            }

            recordToDelete.IsActive = false;
            recordToDelete.LastUpdateByUserFk = updatedByUserId;
            recordToDelete.LastUpdateDate = actionDate;

            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteApplicationAsync));
            throw;
        }
    }

    public async Task<List<ApplicationDTO>> GetAllApplicationListAsync(Expression<Func<Application, bool>> filters, DataBaseServiceContext? context = null)
    {
        List<ApplicationDTO> applicationDTOList = new();

        try
        {
            context = dataBaseContextFactory.CreateDbContext();

            var appicationList = await context.Applications
                .Include(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .Where(filters).ToListAsync();
            applicationDTOList = appicationList.Select(x => x.ToApplicationDTO()).ToList().OrderBy(x => x.ApplicationOwnerId).ThenBy(x => x.Id).ToList();

        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetAllApplicationListAsync));
        }

        return applicationDTOList;
    }


    public async Task<List<ApplicationDTO>> GetApplicationListAsync(Expression<Func<CompanyMembership, bool>> filters, DataBaseServiceContext? context = null)
    {
        List<ApplicationDTO> applicationDTOList = new();

        try
        {
            context = dataBaseContextFactory.CreateDbContext();
            var companyMembershipList = await context.CompanyMemberships
                .Include(x => x.CompanyFkNavigation)
                .Include(x => x.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .Where(filters).ToListAsync();
            applicationDTOList = companyMembershipList.Select(x => x.ToApplicationDTO()).ToList().OrderBy(x => x.CompanyId).ThenBy(x => x.ApplicationOwnerId).ThenBy(x => x.Id).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetApplicationListAsync));
        }

        return applicationDTOList;
    }


    public async Task<RoleDTO> GetRolePermissionListAsync(Expression<Func<Role, bool>> filters, DataBaseServiceContext? context = null)
    {
        var returnList = new RoleDTO();
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var bdList = await context.Roles
                .Include(x => x.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .Include(x => x.RolePermissions)
                .ThenInclude(x => x.ModuleFkNavigation)
                .ThenInclude(x => x.Endpoints)
                .ThenInclude(x => x.Components).FirstOrDefaultAsync(filters);
            returnList = bdList.ToRoleDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetRolePermissionListAsync));
            throw;
        }

        return returnList!;
    }

    public async Task<List<RoleDTO>> GetRoleListByApplicationIdAsync(Expression<Func<Role, bool>> filters, DataBaseServiceContext? context = null)
    {
        var returnList = new List<RoleDTO>();
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var bdList = await context.Roles
                .Include(x => x.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .Include(x => x.RolePermissions)
                .ThenInclude(x => x.ModuleFkNavigation)
                .ThenInclude(x => x.Endpoints)
                .ThenInclude(x => x.Components)
                .Where(filters)
                .ToListAsync();
            returnList = bdList.Select(x => x.ToRoleDTO()!).ToList()
                .OrderBy(x => x.ApplicationOwnerId)
                .ThenBy(x => x.ApplicationFk)
                .ThenBy(x => x.Id)
                .ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetRoleListByApplicationIdAsync));
            throw;
        }

        return returnList;
    }

    public async Task<List<ModuleDTO>> GetModuleListAsync(Expression<Func<Module, bool>> filters, DataBaseServiceContext? context = null)
    {
        var returnList = new List<ModuleDTO>();
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var bdList = await context.Modules
                .Include(x => x.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .Where(filters).ToListAsync();
            returnList = bdList.Select(x => x.ToModuleDTO()).ToList().OrderBy(x => x.ApplicationOwnerId).ThenBy(x => x.ApplicationFk).ThenBy(x => x.PositionIndex).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetModuleListAsync));
            throw;
        }

        return returnList;
    }

    public async Task<ModuleDTO?> CreateNewModuleAsync(ModuleDTO newRecord, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        ModuleDTO? savedRecord = null;
        var newRecordDb = newRecord.ToModule();

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            context.Modules.Add(newRecordDb);
            await context.SaveChangesAsync();
            
            // Reload with navigation properties for ToModuleDTO
            var savedModule = await context.Modules
                .Include(x => x.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .FirstOrDefaultAsync(x => x.Id == newRecordDb.Id);
            
            savedRecord = savedModule?.ToModuleDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewModuleAsync));
        }
        return savedRecord;
    }

    public async Task<ModuleDTO?> UpdateModuleByIdAsync(ModuleDTO recordToUpdate, DataBaseServiceContext? context = null)
    {
        ModuleDTO? savedRecord = null;

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var recordToUpdateDb = await context.Modules
                .Include(x => x.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .FirstOrDefaultAsync(x => x.Id == recordToUpdate.Id);
            if (recordToUpdateDb is null)
            {
                return null;
            }

            recordToUpdateDb.NameTranslationKey = recordToUpdate.NameTranslationKey;
            recordToUpdateDb.DescriptionTranslationKey = recordToUpdate.DescriptionTranslationKey;
            recordToUpdateDb.PositionIndex = recordToUpdate.PositionIndex;
            recordToUpdateDb.IsActive = recordToUpdate.IsActive;
            recordToUpdateDb.IsVisible = recordToUpdate.IsVisible;
            recordToUpdateDb.IsApi = recordToUpdate.IsApi;

            await context.SaveChangesAsync();
            savedRecord = recordToUpdateDb.ToModuleDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateModuleByIdAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<bool> DeleteModuleByIdAsync(long moduleId, long updatedByUserId, DateTime actionDate, DataBaseServiceContext? context = null)
    {
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var recordToDelete = await context.Modules.FirstOrDefaultAsync(x => x.Id == moduleId);
            if (recordToDelete is null)
            {
                return false;
            }

            recordToDelete.IsActive = false;

            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteModuleByIdAsync));
            throw;
        }
    }

    public async Task<List<EndpointDTO>> GetEndpointsListAsync(Expression<Func<Endpoint, bool>> filters, DataBaseServiceContext? context = null)
    {
        var returnList = new List<EndpointDTO>();
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var bdList = await context.Endpoints
                .Include(x => x.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .Where(filters).ToListAsync();
            returnList = bdList.Select(x => x.ToEndpointDTO()).ToList().OrderBy(x => x.ApplicationOwnerId).ThenBy(x => x.ApplicationFk).ThenBy(x => x.PositionIndex).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetEndpointsListAsync));
            throw;
        }

        return returnList;
    }

    /// <summary>
    ///  Get Role permissions If have vaue on EndPointFk  check enpoint  if use wildcard  EndPointFk=null is like * that means have permission on all module end points
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task<List<EndpointDTO>> GetEndpointsListByRoleIdAsync(long roleId, DataBaseServiceContext? context = null)
    {
        var returnList = new List<EndpointDTO>();
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var endpointPermissionList = await context.RolePermissions
               .Include(x => x.ModuleFkNavigation)
               .ThenInclude(x => x.Endpoints)
               .ThenInclude(x => x.Components)
               .Where(r => r.RoleFk == roleId
               && r.RoleFkNavigation.IsActive == true
               && r.ModuleFkNavigation.IsActive == true
               && r.EndpointFkNavigation!.IsActive == true).Select(x => x.EndpointFkNavigation).ToListAsync();

            var endpointModulesWildCardPermissionList = await context.RolePermissions
               .Include(x => x.ModuleFkNavigation)
               .ThenInclude(x => x.Endpoints)
               .ThenInclude(x => x.Components)
               .Where(r => r.RoleFk == roleId
               && r.RoleFkNavigation.IsActive == true
               && r.ModuleFkNavigation.IsActive == true
               && r.EndpointFk == null).Select(x => x.ModuleFkNavigation.Endpoints).ToListAsync();

            endpointModulesWildCardPermissionList.ForEach(itemModule =>
            {
                endpointPermissionList.AddRange(itemModule.ToList());
            });
            endpointPermissionList = endpointPermissionList.Distinct().ToList();

            returnList = endpointPermissionList!.Select(x => x.ToEndpointDTO()).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetEndpointsListAsync));
            throw;
        }

        return returnList;
    }

    public async Task<List<ComponentDTO>> GetComponentListAsync(Expression<Func<Component, bool>> filters, DataBaseServiceContext? context = null)
    {
        var returnList = new List<ComponentDTO>();
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var bdList = await context.Components
                .Include(x => x.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .Where(filters).ToListAsync();
            returnList = bdList.Select(x => x.ToComponentDTO ()).ToList().OrderBy(x => x.ApplicationOwnerId).ThenBy(x => x.ApplicationFk).ThenBy(x => x.PositionIndex).ToList(); ;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetComponentListAsync));
            throw;
        }

        return returnList;
    }

    public async Task<EndpointDTO?> CreateNewEndpointAsync(EndpointDTO newRecord, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        EndpointDTO? savedRecord = null;
        var newRecordDb = newRecord.ToEndpoint();

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            context.Endpoints.Add(newRecordDb);
            await context.SaveChangesAsync();
            
            // Reload with navigation properties for ToEndpointDTO
            var savedEndpoint = await context.Endpoints
                .Include(x => x.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .FirstOrDefaultAsync(x => x.Id == newRecordDb.Id);
            
            savedRecord = savedEndpoint?.ToEndpointDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewEndpointAsync));
        }
        return savedRecord;
    }

    public async Task<EndpointDTO?> UpdateEndpointByIdAsync(EndpointDTO recordToUpdate, DataBaseServiceContext? context = null)
    {
        EndpointDTO? savedRecord = null;

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var recordToUpdateDb = await context.Endpoints
                .Include(x => x.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .FirstOrDefaultAsync(x => x.Id == recordToUpdate.Id);
            if (recordToUpdateDb is null)
            {
                return null;
            }

            recordToUpdateDb.NameTranslationKey = recordToUpdate.NameTranslationKey;
            recordToUpdateDb.DescriptionTranslationKey = recordToUpdate.DescriptionTranslationKey;
            recordToUpdateDb.PositionIndex = recordToUpdate.PositionIndex;
            recordToUpdateDb.IsActive = recordToUpdate.IsActive;
            recordToUpdateDb.IsVisible = recordToUpdate.IsVisible;
            recordToUpdateDb.IsApi = recordToUpdate.IsApi;
            recordToUpdateDb.EndpointUrl = recordToUpdate.EndpointUrl;
            recordToUpdateDb.Method = recordToUpdate.Method;

            await context.SaveChangesAsync();
            savedRecord = recordToUpdateDb.ToEndpointDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateEndpointByIdAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<bool> DeleteEndpointByIdAsync(long endpointId, long updatedByUserId, DateTime actionDate, DataBaseServiceContext? context = null)
    {
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var recordToDelete = await context.Endpoints.FirstOrDefaultAsync(x => x.Id == endpointId);
            if (recordToDelete is null)
            {
                return false;
            }

            recordToDelete.IsActive = false;

            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteEndpointByIdAsync));
            throw;
        }
    }

    public async Task<ComponentDTO?> CreateNewComponentAsync(ComponentDTO newRecord, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        ComponentDTO? savedRecord = null;
        var newRecordDb = newRecord.ToComponent();

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            context.Components.Add(newRecordDb);
            await context.SaveChangesAsync();
            
            // Reload with navigation properties for ToComponentDTO
            var savedComponent = await context.Components
                .Include(x => x.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .FirstOrDefaultAsync(x => x.Id == newRecordDb.Id);
            
            savedRecord = savedComponent?.ToComponentDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewComponentAsync));
        }
        return savedRecord;
    }

    public async Task<ComponentDTO?> UpdateComponentByIdAsync(ComponentDTO recordToUpdate, DataBaseServiceContext? context = null)
    {
        ComponentDTO? savedRecord = null;

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var recordToUpdateDb = await context.Components
                .Include(x => x.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .FirstOrDefaultAsync(x => x.Id == recordToUpdate.Id);
            if (recordToUpdateDb is null)
            {
                return null;
            }

            recordToUpdateDb.NameTranslationKey = recordToUpdate.NameTranslationKey;
            recordToUpdateDb.DescriptionTranslationKey = recordToUpdate.DescriptionTranslationKey;
            recordToUpdateDb.ObjectId = recordToUpdate.ObjectId;
            recordToUpdateDb.ObjectName = recordToUpdate.ObjectName;
            recordToUpdateDb.ObjectPropertyName = recordToUpdate.ObjectPropertyName;
            recordToUpdateDb.DefaultPropertyValue = recordToUpdate.DefaultPropertyValue;
            recordToUpdateDb.PositionIndex = recordToUpdate.PositionIndex;

            await context.SaveChangesAsync();
            savedRecord = recordToUpdateDb.ToComponentDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateComponentByIdAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<bool> DeleteComponentByIdAsync(long componentId, long updatedByUserId, DateTime actionDate, DataBaseServiceContext? context = null)
    {
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var recordToDelete = await context.Components.FirstOrDefaultAsync(x => x.Id == componentId);
            if (recordToDelete is null)
            {
                return false;
            }

            context.Components.Remove(recordToDelete);

            await context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteComponentByIdAsync));
            throw;
        }
    }

}

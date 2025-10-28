using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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

}

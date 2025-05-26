using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Options;
using Vito.Transverse.Identity.DAL.DataBaseContext;
using Vito.Transverse.Identity.DAL.DataBaseContextFactory;
using Vito.Transverse.Identity.Domain.DTO;
using Vito.Transverse.Identity.Domain.Extensions;
using Vito.Transverse.Identity.Domain.Models;
using Vito.Transverse.Identity.Domain.ModelsDTO;
using Vito.Transverse.Identity.Domain.Options;

namespace Vito.Transverse.Identity.DAL.TransverseRepositories.Security;



public class SecurityRepository(IDataBaseContextFactory dataBaseContextFactory, IOptions<IdentityServiceServerSettingsOptions> identityServiceOptions, IOptions<EncryptionSettingsOptions> encryptionSettingsOptions, ILogger<SecurityRepository> logger) : ISecurityRepository
{
    private IdentityServiceServerSettingsOptions identityServiceOptionsValues = identityServiceOptions.Value;
    private EncryptionSettingsOptions encryptionSettingsOptionsValues = encryptionSettingsOptions.Value;

    #region Public Methods


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

    public async Task<CompanyDTO?> CreateNewCompanyAsync(CompanyApplicationsDTO newRecord, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        CompanyDTO? savedRecord = null;
        var newRecordDb = newRecord.Company.ToCompany();

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            context.Companies.Add(newRecordDb);
            await context.SaveChangesAsync();
            savedRecord = newRecordDb.ToCompanyDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewApplicationAsync));
        }
        return savedRecord;
    }

    public async Task<CompanyDTO?> UpdateCompanyApplicationsAsync(CompanyApplicationsDTO recordToUpdate, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        CompanyDTO? savedRecord = null;

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var recordToUpdateDb = await context.Companies.FirstOrDefaultAsync(x => x.Id == recordToUpdate.Company.Id);
            recordToUpdateDb = recordToUpdate.Company.ToCompany(); ;
            await context.SaveChangesAsync();
            savedRecord = recordToUpdateDb!.ToCompanyDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateCompanyApplicationsAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<UserDTO?> CreateNewUserAsync(UserDTO newRecord, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        UserDTO? savedRecord = null;
        var newRecordDb = newRecord.ToUser();

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            context.Users.Add(newRecordDb);
            await context.SaveChangesAsync();
            savedRecord = newRecordDb.ToUserDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewUserAsync));
        }
        return savedRecord;
    }


    public async Task<UserDTO?> UpdateUserAsync(UserDTO recordToUpdate, DataBaseServiceContext? context = null)
    {
        UserDTO? savedRecord = null;

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var recordToUpdateDb = await context.Users.FirstOrDefaultAsync(x => x.Id == recordToUpdate.Id);
            recordToUpdateDb = recordToUpdate.ToUser();
            await context.SaveChangesAsync();
            savedRecord = recordToUpdateDb!.ToUserDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateCompanyApplicationsAsync));
            throw;
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
            applicationDTOList = appicationList.ToApplicationDTOList().OrderBy(x => x.ApplicationOwnerId).ThenBy(x => x.Id).ToList();

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
            applicationDTOList = companyMembershipList.ToApplicationDTOList().OrderBy(x => x.CompanyId).ThenBy(x => x.ApplicationOwnerId).ThenBy(x => x.Id).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetApplicationListAsync));
        }

        return applicationDTOList;
    }

    public async Task<List<CompanyMembershipsDTO>> GetCompanyMemberhipListAsync(Expression<Func<CompanyMembership, bool>> filters, DataBaseServiceContext? context = null)
    {
        List<CompanyMembershipsDTO> applicationDTOList = new();

        try
        {
            context = dataBaseContextFactory.CreateDbContext();
            var companyMembershipsList = await context.CompanyMemberships
                .Include(x => x.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .Include(x => x.MembershipTypeFkNavigation)
                .Include(x => x.CompanyFkNavigation)
                .Where(filters).ToListAsync();
            applicationDTOList = companyMembershipsList.ToCompanyMembershipsDTOList().OrderBy(x => x.CompanyFk).ThenBy(x => x.ApplicationOwnerId).ThenBy(x => x.Id).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetApplicationListAsync));
        }

        return applicationDTOList;
    }

    public async Task<List<CompanyDTO>> GetAllCompanyListAsync(Expression<Func<Company, bool>> filters, DataBaseServiceContext? context = null)
    {
        List<CompanyDTO> companyDTOList = new();

        try
        {
            context = dataBaseContextFactory.CreateDbContext();
            var companyList = await context.Companies

                .Include(x => x.CountryFkNavigation)
                .Include(x => x.DefaultCultureFkNavigation)
                .ThenInclude(x => x.LanguageFkNavigation)
                .Where(filters)
                .ToListAsync();
            companyDTOList = companyList.ToCompanyDTOList().OrderBy(x => x.Id).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetAllCompanyListAsync));
        }

        return companyDTOList;
    }

    public async Task<List<RoleDTO>> GetRoleListAsync(Expression<Func<Role, bool>> filters, DataBaseServiceContext? context = null)
    {
        var returnList = new List<RoleDTO>();
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var bdList = await context.Roles
                .Include(x => x.CompanyFkNavigation)
                .Include(x => x.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .Where(filters).ToListAsync();
            returnList = bdList.ToRoleDTOList().OrderBy(x => x.CompanyFk).ThenBy(x => x.ApplicationOwnerId).ThenBy(x => x.ApplicationFk).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetRoleListAsync));
            throw;
        }

        return returnList;
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
            returnList = bdList.ToModuleDTOList().OrderBy(x => x.ApplicationOwnerId).ThenBy(x => x.ApplicationFk).ThenBy(x => x.PositionIndex).ToList();
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
            returnList = bdList.ToEndpointDTOList().OrderBy(x => x.ApplicationOwnerId).ThenBy(x => x.ApplicationFk).ThenBy(x => x.PositionIndex).ToList();
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

            returnList = endpointPermissionList!.ToEndpointDTOList();
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
            returnList = bdList.ToComponentDTOList().OrderBy(x => x.ApplicationOwnerId).ThenBy(x => x.ApplicationFk).ThenBy(x => x.PositionIndex).ToList(); ;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetComponentListAsync));
            throw;
        }

        return returnList;
    }

    public async Task<List<UserRoleDTO>> GetUserRolesListAsync(Expression<Func<UserRole, bool>> filters, DataBaseServiceContext? context = null)
    {
        var returnList = new List<UserRoleDTO>();
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var bdList = await context.UserRoles
                .Include(x => x.UserFkNavigation)
                .Include(x => x.RoleFkNavigation.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .Include(x => x.RoleFkNavigation.CompanyFkNavigation)
                .Where(filters).ToListAsync();
            returnList = bdList.ToUserRoleDTOList().OrderBy(x => x.CompanyFk).ThenBy(x => x.ApplicationOwnerId).ThenBy(x => x.RoleFk).ToList(); ;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetUserRolesListAsync));
            throw;
        }

        return returnList;
    }

    public async Task<UserDTO> GetUserPermissionListAsync(Expression<Func<User, bool>> filters, DataBaseServiceContext? context = null)
    {
        var returnValue = new UserDTO();
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var bdList = await context.Users
                .Include(x => x.UserRoles)
                    .ThenInclude(x => x.RoleFkNavigation)
                    .ThenInclude(x => x.ApplicationFkNavigation)
                    .ThenInclude(x => x.ApplicationOwners)
                    .ThenInclude(x => x.CompanyFkNavigation)
                .Include(x => x.UserRoles)
                    .ThenInclude(x => x.RoleFkNavigation)
                    .ThenInclude(x => x.RolePermissions)
                    .ThenInclude(x => x.ModuleFkNavigation)
                    .ThenInclude(x => x.Endpoints)
                    .ThenInclude(x => x.Components)
                .FirstOrDefaultAsync(filters);
            returnValue = bdList!.ToUserDTO()!;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetUserPermissionListAsync));
            throw;
        }

        return returnValue;
    }

    public async Task<List<UserDTO>> GetUserListAsync(Expression<Func<User, bool>> filters, DataBaseServiceContext? context = null)
    {
        var returnList = new List<UserDTO>();
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var bdList = await context.Users
                .Include(x => x.UserRoles)
                    .ThenInclude(x => x.RoleFkNavigation)
                    .ThenInclude(x => x.ApplicationFkNavigation)
                    .ThenInclude(x => x.ApplicationOwners)
                    .ThenInclude(x => x.CompanyFkNavigation)
                .Include(x => x.CompanyFkNavigation)
                //.Include(x => x.UserRoles)
                //    .ThenInclude(x => x.RoleFkNavigation)
                //    .ThenInclude(x => x.RolePermissions)
                //    .ThenInclude(x => x.ModuleFkNavigation)
                //    .ThenInclude(x => x.Endpoints)
                //    .ThenInclude(x => x.Components)
                .Where(filters).ToListAsync();
            returnList = bdList!.ToUserDTOList()!;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetUserPermissionListAsync));
            throw;
        }

        return returnList;
    }


    #endregion

    #region Private Methods


    #endregion
}
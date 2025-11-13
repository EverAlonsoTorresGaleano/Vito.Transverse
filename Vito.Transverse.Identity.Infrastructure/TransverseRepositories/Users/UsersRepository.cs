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

namespace  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Users;

public class UsersRepository(ILogger<SecurityRepository> logger, IDataBaseContextFactory dataBaseContextFactory) : IUsersRepository
{

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
            logger.LogError(ex, message: nameof(UpdateUserAsync));
            throw;
        }

        return savedRecord;
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
            returnList = bdList!.Select(x => x.ToRoleDTO()).ToList().OrderBy(x => x.CompanyFk).ThenBy(x => x.ApplicationOwnerId).ThenBy(x => x.ApplicationFk).ToList();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetRoleListAsync));
            throw;
        }

        return returnList;
    }

    public async Task<RoleDTO?> GetRoleByIdAsync(long roleId, DataBaseServiceContext? context = null)
    {
        RoleDTO? roleDTO = null;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var role = await context.Roles
                .Include(x => x.CompanyFkNavigation)
                .Include(x => x.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .FirstOrDefaultAsync(x => x.Id == roleId);
            roleDTO = role?.ToRoleDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetRoleByIdAsync));
            throw;
        }

        return roleDTO;
    }

    public async Task<RoleDTO?> UpdateRoleAsync(RoleDTO recordToUpdate, DataBaseServiceContext? context = null)
    {
        RoleDTO? savedRecord = null;

        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var roleToUpdate = await context.Roles.FirstOrDefaultAsync(x => x.Id == recordToUpdate.Id);
            if (roleToUpdate is null)
            {
                return null;
            }

            var updatedRole = recordToUpdate.ToRole();
            context.Entry(roleToUpdate).CurrentValues.SetValues(updatedRole);
            await context.SaveChangesAsync();
            savedRecord = roleToUpdate.ToRoleDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateRoleAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<bool> DeleteRoleAsync(long roleId, DataBaseServiceContext? context = null)
    {
        bool deleted = false;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var role = await context.Roles.FirstOrDefaultAsync(x => x.Id == roleId);
            if (role is null)
            {
                return false;
            }

            context.Roles.Remove(role);
            await context.SaveChangesAsync();
            deleted = true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteRoleAsync));
            throw;
        }

        return deleted;
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
            returnList = bdList.Select(x=>x.ToUserRoleDTO()).ToList().OrderBy(x => x.CompanyFk).ThenBy(x => x.ApplicationOwnerId).ThenBy(x => x.RoleFk).ToList(); ;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetUserRolesListAsync));
            throw;
        }

        return returnList;
    }

    public async Task<UserRoleDTO?> GetUserRoleByIdAsync(long userId, long roleId, long companyFk, long applicationFk, DataBaseServiceContext? context = null)
    {
        UserRoleDTO? userRoleDTO = null;
        try
        {
            context = dataBaseContextFactory.CreateDbContext();
            var userRole = await context.UserRoles
                .Include(x => x.UserFkNavigation)
                .Include(x => x.RoleFkNavigation.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .Include(x => x.RoleFkNavigation.CompanyFkNavigation)
                .FirstOrDefaultAsync(x => x.UserFk == userId && x.RoleFk == roleId && x.CompanyFk == companyFk && x.ApplicationFk == applicationFk);
            userRoleDTO = userRole?.ToUserRoleDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetUserRoleByIdAsync));
            throw;
        }

        return userRoleDTO;
    }

    public async Task<UserRoleDTO?> CreateNewUserRoleAsync(UserRoleDTO userRoleInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        UserRoleDTO? savedRecord = null;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var newUserRole = new UserRole
            {
                CompanyFk = userRoleInfo.CompanyFk,
                ApplicationFk = userRoleInfo.ApplicationFk,
                UserFk = userRoleInfo.UserFk,
                RoleFk = userRoleInfo.RoleFk,
                CreatedDate = DateTime.UtcNow,
                CreatedByUserFk = deviceInformation.UserId ,
                IsActive = userRoleInfo.IsActive
            };

            context.UserRoles.Add(newUserRole);
            await context.SaveChangesAsync();

            // Reload with includes to get navigation properties
            newUserRole = await context.UserRoles
                .Include(x => x.UserFkNavigation)
                .Include(x => x.RoleFkNavigation.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .Include(x => x.RoleFkNavigation.CompanyFkNavigation)
                .FirstOrDefaultAsync(x => x.UserFk == userRoleInfo.UserFk && x.RoleFk == userRoleInfo.RoleFk && x.CompanyFk == userRoleInfo.CompanyFk && x.ApplicationFk == userRoleInfo.ApplicationFk);

            savedRecord = newUserRole?.ToUserRoleDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(CreateNewUserRoleAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<UserRoleDTO?> UpdateUserRoleByIdAsync(UserRoleDTO userRoleInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        UserRoleDTO? savedRecord = null;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var userRoleToUpdate = await context.UserRoles
                .FirstOrDefaultAsync(x => x.UserFk == userRoleInfo.UserFk && x.RoleFk == userRoleInfo.RoleFk && x.CompanyFk == userRoleInfo.CompanyFk && x.ApplicationFk == userRoleInfo.ApplicationFk);
            
            if (userRoleToUpdate is null)
            {
                return null;
            }

            userRoleToUpdate.IsActive = userRoleInfo.IsActive;
            userRoleToUpdate.CreatedByUserFk = userRoleInfo.CreatedByUserFk;

            await context.SaveChangesAsync();

            // Reload with includes to get navigation properties
            userRoleToUpdate = await context.UserRoles
                .Include(x => x.UserFkNavigation)
                .Include(x => x.RoleFkNavigation.ApplicationFkNavigation)
                .ThenInclude(x => x.ApplicationOwners)
                .ThenInclude(x => x.CompanyFkNavigation)
                .Include(x => x.RoleFkNavigation.CompanyFkNavigation)
                .FirstOrDefaultAsync(x => x.UserFk == userRoleInfo.UserFk && x.RoleFk == userRoleInfo.RoleFk && x.CompanyFk == userRoleInfo.CompanyFk && x.ApplicationFk == userRoleInfo.ApplicationFk);

            savedRecord = userRoleToUpdate?.ToUserRoleDTO();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(UpdateUserRoleByIdAsync));
            throw;
        }

        return savedRecord;
    }

    public async Task<bool> DeleteUserRoleByIdAsync(long userId, long roleId, long companyFk, long applicationFk, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null)
    {
        bool deleted = false;
        try
        {
            context = dataBaseContextFactory.GetDbContext(context);
            var userRole = await context.UserRoles
                .FirstOrDefaultAsync(x => x.UserFk == userId && x.RoleFk == roleId && x.CompanyFk == companyFk && x.ApplicationFk == applicationFk);
            
            if (userRole is null)
            {
                return false;
            }

            context.UserRoles.Remove(userRole);
            await context.SaveChangesAsync();
            deleted = true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(DeleteUserRoleByIdAsync));
            throw;
        }

        return deleted;
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


    public async Task<List<MenuGroupDTO>> GetUserMenuByUserIdAsync(Expression<Func<User, bool>> filters, DataBaseServiceContext? context = null)
    {
        var returnValue = new List<MenuGroupDTO>();
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
            returnValue = bdList!.ToMenuGroupDTOList();
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
            returnList = bdList!.Select(x=>x.ToUserDTO()).ToList()!;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, message: nameof(GetUserPermissionListAsync));
            throw;
        }

        return returnList;
    }

}

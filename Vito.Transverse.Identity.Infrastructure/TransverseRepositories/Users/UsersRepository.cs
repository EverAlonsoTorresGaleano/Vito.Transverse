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

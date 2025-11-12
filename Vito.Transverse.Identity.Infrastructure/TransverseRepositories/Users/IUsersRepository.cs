using System.Linq.Expressions;
using Vito.Framework.Common.DTO;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using  Vito.Transverse.Identity.Infrastructure.Models;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace  Vito.Transverse.Identity.Infrastructure.TransverseRepositories.Users;

public interface IUsersRepository
{


    Task<UserDTO?> CreateNewUserAsync(UserDTO userInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<UserDTO?> UpdateUserAsync(UserDTO recordToUpdate, DataBaseServiceContext? context = null);
    Task<List<RoleDTO>> GetRoleListAsync(Expression<Func<Role, bool>> filters, DataBaseServiceContext? context = null);
    Task<RoleDTO?> GetRoleByIdAsync(long roleId, DataBaseServiceContext? context = null);
    Task<RoleDTO?> UpdateRoleAsync(RoleDTO recordToUpdate, DataBaseServiceContext? context = null);
    Task<bool> DeleteRoleAsync(long roleId, DataBaseServiceContext? context = null);
    Task<List<UserRoleDTO>> GetUserRolesListAsync(Expression<Func<UserRole, bool>> filters, DataBaseServiceContext? context = null);

    Task<UserRoleDTO?> GetUserRoleByIdAsync(long userId, long roleId, long companyFk, long applicationFk, DataBaseServiceContext? context = null);

    Task<UserRoleDTO?> CreateNewUserRoleAsync(UserRoleDTO userRoleInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<UserRoleDTO?> UpdateUserRoleByIdAsync(UserRoleDTO userRoleInfo, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<bool> DeleteUserRoleByIdAsync(long userId, long roleId, long companyFk, long applicationFk, DeviceInformationDTO deviceInformation, DataBaseServiceContext? context = null);

    Task<UserDTO> GetUserPermissionListAsync(Expression<Func<User, bool>> filters, DataBaseServiceContext? context = null);

    Task<List<UserDTO>> GetUserListAsync(Expression<Func<User, bool>> filters, DataBaseServiceContext? context = null);


}

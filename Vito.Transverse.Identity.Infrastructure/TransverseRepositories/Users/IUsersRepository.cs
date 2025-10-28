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
    Task<List<UserRoleDTO>> GetUserRolesListAsync(Expression<Func<UserRole, bool>> filters, DataBaseServiceContext? context = null);

    Task<UserDTO> GetUserPermissionListAsync(Expression<Func<User, bool>> filters, DataBaseServiceContext? context = null);

    Task<List<UserDTO>> GetUserListAsync(Expression<Func<User, bool>> filters, DataBaseServiceContext? context = null);


}

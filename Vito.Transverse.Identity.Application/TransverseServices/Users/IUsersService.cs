using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vito.Framework.Common.DTO;
using Vito.Framework.Common.Enums;
using  Vito.Transverse.Identity.Infrastructure.DataBaseContext;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace  Vito.Transverse.Identity.Application.TransverseServices.Users;

public interface IUsersService
{

    Task<UserDTO?> CreateNewUserAsync(long companyId, UserDTO newRecord, DeviceInformationDTO deviceInformation);

    Task<UserDTO?> ChangeUserPasswordAsync(UserDTO userInfo, DeviceInformationDTO deviceInformation);

    Task<UserDTO?> UpdateLastUserAccessAsync(long userId, DeviceInformationDTO deviceInformation, OAuthActionTypeEnum actionStatus, DataBaseServiceContext? context = null);

    Task<UserDTO?> SendActivationEmailAsync(long companyId, long userId, DeviceInformationDTO deviceInformation);

    Task<bool?> ActivateAccountAsync(string activationToken, DeviceInformationDTO deviceInformation);


    Task<List<UserRoleDTO>> GetUserRolesListAsync(long userId);

    Task<UserDTO> GetUserPermissionListAsync(long userId);

    Task<List<UserDTO>> GetUserListAsync(long? companyId);

    Task<List<RoleDTO>> GetRoleListAsync(long? companyId);
}

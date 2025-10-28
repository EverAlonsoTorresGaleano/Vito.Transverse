using Vito.Framework.Common.Enums;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace Vito.Transverse.Identity.Entities.Extensions;

public static class MapperDTOExtension
{
    public static UserDTOToken ToUserDTOToken(this UserDTO modelEntity, ApplicationDTO? applicationEntity, UserRoleDTO? roleEntity, OAuthActionTypeEnum actionStatus = OAuthActionTypeEnum.OAuthActionType_Undefined)
    {
        UserDTOToken returnObject = new()
        {

            Id = modelEntity.Id,
            UserName = modelEntity.UserName,
            Password = null!,
            EmailValidated = modelEntity.EmailValidated,
            IsLocked = modelEntity.IsLocked,
            RequirePasswordChange = modelEntity.RequirePasswordChange,
            RetryCount = modelEntity.RetryCount,
            LastAccess = modelEntity.LastAccess,
            IsActive = modelEntity.IsActive,

            Name = modelEntity.Name,
            LastName = modelEntity.LastName,
            Email = modelEntity.Email,

            ApplicationOwnerId = applicationEntity!.ApplicationOwnerId,
            ApplicationOwnerNameTranslationKey = applicationEntity.ApplicationOwnerNameTranslationKey,

            ApplicationId = applicationEntity.Id,
            ApplicationNameTranslationKey = applicationEntity.NameTranslationKey,

            CompanyFk = modelEntity.CompanyFk,
            CompanyNameTranslationKey = modelEntity.CompanyNameTranslationKey,

            RoleId = roleEntity?.RoleFk,
            RoleName = roleEntity?.RoleNameTranslationKey,
            ActionStatus = actionStatus
        };
        return returnObject;
    }
}

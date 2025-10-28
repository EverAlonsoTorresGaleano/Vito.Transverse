

using Vito.Framework.Common.Enums;

namespace Vito.Transverse.Identity.Entities.ModelsDTO;

public class UserDTOToken : UserDTO
{
    public long? ApplicationOwnerId { get; set; }
    public string? ApplicationOwnerNameTranslationKey { get; set; }

    public long? ApplicationId { get; set; }
    public string? ApplicationNameTranslationKey { get; set; }

    public long? RoleId { get; set; }
    public string? RoleName { get; set; }

    public OAuthActionTypeEnum? ActionStatus { get; set; }
}
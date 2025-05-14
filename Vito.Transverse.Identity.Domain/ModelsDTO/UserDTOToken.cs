

using Vito.Framework.Common.Enums;

namespace Vito.Transverse.Identity.Domain.ModelsDTO;

public class UserDTOToken:UserDTO
{
    public long? ApplicationId { get; set; }
    public string? ApplicationName { get; set; }

    public Guid? CompanySecret { get; set; }
    public string? CompanyName { get; set; }
    public long? RoleId { get; set; }
    public string? RoleName { get; set; }
    public OAuthActionTypeEnum? ActionStatus { get; set; }
}
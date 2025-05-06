

using Vito.Framework.Common.Enums;

namespace Vito.Transverse.Identity.Domain.ModelsDTO;

public class UserDTO
{
    public long CompanyFk { get; set; }

    public Guid? CompanySecret { get; set; }
    public long Id { get; set; }
    public string? UserName { get; set; }
    public long PersonFk { get; set; }
    public string? Password { get; set; }
    public bool EmailValidated { get; set; }
    public bool IsLocked { get; set; }
    public bool RequirePasswordChange { get; set; }
    public int RetryCount { get; set; }
    public DateTime? LastAccess { get; set; }
    public bool IsActive { get; set; }
    public long RoleFk { get; set; }
    public long? DocumentTypeFk { get; set; }
    public string? DocumentValue { get; set; }
    public string? Name { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public long? GenderFk { get; set; }
    public string? MobileNumber { get; set; }
    public long? ApplicationId { get; set; }
    public string? ApplicationName { get; set; }

    public Guid? ActivationId { get; set; }
    public string? CompanyName { get; set; }
    public string? RoleName { get; set; }

    public ActionTypeEnum? ActionStatus { get; set; }
}
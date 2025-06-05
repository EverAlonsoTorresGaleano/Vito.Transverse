namespace Vito.Transverse.Identity.Domain.Enums;

public enum FeatureFlagsNamesEnum
{
    HomeFeature,
    CultureFeature,
    LocalizationFeature,
    OAuth2Feature,
    CacheFeature,
    MediaFeature,
    AuditFeature,
    HealthFeature
}

public enum CacheItemKeysEnum
{
    All,
    CultureList,
    CultureTranslationsListByApplicationId,
    CultureTranslationsListByApplicationIdCultureId,


    AllApplicationList,
    ApplicationListByCompanyId,
    CompanyMemberhipListByCompanyId,
    AllCompanyList,
    RoleListByCompanyId,
    RolePermissionListByRoleId,
    ModuleListByApplicationId,
    EndpointListByModuleId,
    EndpointListByRoleId,
    ComponentListByEndpointId,
    UserRoleListByUserId,
    UserPermissionListByUserId,
    CompanyEntityAuditListByCompanyId,

    PictureListByCompanyId,
    EntityList,
    UserListByCompanyId,
    NotificationTemplates,
    HealthCheck
}

public enum NotificationTemplatesEnum
{
    ActivationEmail = 1,
}

public enum DataBaseNameEnum
{
    TransverseDB
}


public enum TransverseExceptionEnum
{
    UserPermissionException_ModuleFromApplicationNotFound,
    ActivateAccount_InvalidToken
}


public enum EmailTemplateParametersEnum
{
    EMAIL,
    USER_ID,
    APPLICATION_CLIENTID,
    ACTIVATION_ID,
    FULL_NAME
}
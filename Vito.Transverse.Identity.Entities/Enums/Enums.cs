namespace Vito.Transverse.Identity.Entities.Enums;

public enum FeatureFlagsNamesEnum
{
    CulturesFeature,
    LocalizationsFeature,
    OAuth2Feature,
    CacheFeature,
    MediaFeature,
    AuditsFeature,
    HealthFeature,
    ApplicationsFeature,
    CompaniesFeature,
    UsersFeature,
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
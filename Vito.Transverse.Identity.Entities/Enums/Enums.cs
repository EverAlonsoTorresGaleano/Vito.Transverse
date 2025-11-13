namespace Vito.Transverse.Identity.Entities.Enums;

public enum FeatureFlagsNamesEnum
{
    LocalizationsFeature,
    OAuth2Feature,
    CacheFeature,
    MediaFeature,
    AuditsFeature,
    HealthFeature,
    ApplicationsFeature,
    CompaniesFeature,
    UsersFeature,
    MasterFeature,
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
    RoleListByApplicationId,
    RolePermissionListByRoleId,
    ModuleListByApplicationId,
    EndpointListByModuleId,
    EndpointListByRoleId,
    ComponentListByEndpointId,
    UserRoleListByUserId,
    UserPermissionListByUserId,
    CompanyEntityAuditListByCompanyId,
    MenuByUserId,
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
namespace Vito.Transverse.Identity.Domain.Enums;

public enum FeatureFlagsNamesEnum
{
    HomeFeature,
    CultureFeature,
    LocalizationFeature,
    SecurityFeature,
}

public enum CacheItemKeysEnum
{
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
    ACTIVATION_ID
}
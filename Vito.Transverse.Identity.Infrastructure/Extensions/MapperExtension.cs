using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Extensions;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Transverse.Identity.Entities.Enums;
using Vito.Transverse.Identity.Entities.ModelsDTO;
using Vito.Transverse.Identity.Infrastructure.Models;

namespace Vito.Transverse.Identity.Infrastructure.Extensions;

public static class MapperExtension
{
    private const string commaSeparator = ",";

    public static CultureDTO ToCultureDTO(this Culture modelObject)
    {
        CultureDTO returnObject = new()
        {
            Id = modelObject.Id,
            NameTranslationKey = modelObject.NameTranslationKey,
            //Name = _cultureService.GetLocalizedMessage(modelObject.NameTranslationKey).TranslationValue,
            CountryFk = modelObject.CountryFk,
            LanguageFk = modelObject.LanguageFk,
            IsEnabled = modelObject.IsEnabled
        };
        return returnObject;
    }

    public static Culture ToCulture(this CultureDTO modelObject)
    {
        Culture returnObject = new()
        {
            Id = modelObject.Id,
            NameTranslationKey = modelObject.NameTranslationKey,
            CountryFk = modelObject.CountryFk!,
            LanguageFk = modelObject.LanguageFk!,
            IsEnabled = modelObject.IsEnabled
        };
        return returnObject;
    }

    public static LanguageDTO ToLanguageDTO(this Language modelObject)
    {
        LanguageDTO returnObject = new()
        {
            Id = modelObject.Id,
            NameTranslationKey = modelObject.NameTranslationKey
        };
        return returnObject;
    }

    public static Language ToLanguage(this LanguageDTO modelObject)
    {
        Language returnObject = new()
        {
            Id = modelObject.Id,
            NameTranslationKey = modelObject.NameTranslationKey
        };
        return returnObject;
    }

    public static CountryDTO ToCountryDTO(this Country modelObject)
    {
        CountryDTO returnObject = new()
        {
            Id = modelObject.Id,
            NameTranslationKey = modelObject.NameTranslationKey,
            UtcHoursDifference = modelObject.UtcHoursDifference
        };
        return returnObject;
    }

    public static Country ToCountry(this CountryDTO modelObject)
    {
        Country returnObject = new()
        {
            Id = modelObject.Id,
            NameTranslationKey = modelObject.NameTranslationKey,
            UtcHoursDifference = modelObject.UtcHoursDifference
        };
        return returnObject;
    }

    public static CultureTranslationDTO ToCultureTranslationDTO(this CultureTranslation modelObject)
    {
        CultureTranslationDTO returnObject = new()
        {
            ApplicationFk = modelObject.ApplicationFk,
            CultureFk = modelObject.CultureFk,
            TranslationKey = modelObject.TranslationKey,
            TranslationValue = modelObject.TranslationValue,
            ApplicationNameTranslationKey = modelObject.ApplicationFkNavigation.NameTranslationKey,
            LanguageNameTranslationKey = modelObject.CultureFkNavigation.LanguageFkNavigation.NameTranslationKey
        };
        return returnObject;
    }


    public static ListItemDTO ToListItemDTO(this GeneralTypeItem modelObject)
    {
        ListItemDTO returnObject = new()
        {
            Id = modelObject.Id.ToString(),
            IsEnabled = modelObject.IsEnabled,
            NameTranslationKey = modelObject.NameTranslationKey,
            OrderIndex = modelObject.OrderIndex,
            ListItemGroupFk = modelObject.ListItemGroupFk.ToString(),
        };
        return returnObject;
    }


    public static ListItemDTO ToListItemDTO(this CultureDTO modelObject)
    {
        ListItemDTO returnObject = new()
        {
            Id = modelObject.Id,
            IsEnabled = modelObject.IsEnabled,
            NameTranslationKey = modelObject.NameTranslationKey,
            ListItemGroupFk = modelObject.LanguageFk!
        };
        return returnObject;
    }

    public static ListItemDTO ToListItemDTO(this UserDTO modelObject)
    {
        ListItemDTO returnObject = new()
        {
            Id = modelObject.Id.ToString(),
            IsEnabled = true,
            NameTranslationKey = $"{modelObject.Name} {modelObject.LastName} [{modelObject.Email}]",
            ListItemGroupFk = string.Empty
        };
        return returnObject;
    }

    public static ListItemDTO ToListItemDTO(this RoleDTO modelObject)
    {
        ListItemDTO returnObject = new()
        {
            Id = modelObject.Id.ToString(),
            IsEnabled = true,
            NameTranslationKey = modelObject.NameTranslationKey,
            ListItemGroupFk = string.Empty
        };
        return returnObject;
    }

    public static ListItemDTO ToListItemDTO(this ApplicationDTO modelObject)
    {
        ListItemDTO returnObject = new()
        {
            Id = modelObject.ApplicationClient.ToString(),
            IsEnabled = true,
            NameTranslationKey = modelObject.NameTranslationKey,
            ListItemGroupFk = string.Empty
        };
        return returnObject;
    }

    public static ListItemDTO ToListItemDTO(this CompanyDTO modelObject)
    {
        ListItemDTO returnObject = new()
        {
            Id = modelObject.CompanyClient.ToString(),
            IsEnabled = true,
            NameTranslationKey = modelObject.NameTranslationKey,
            ListItemGroupFk = string.Empty
        };
        return returnObject;
    }

    public static ListItemDTO ToListItemDTO(this CountryDTO modelObject)
    {
        ListItemDTO returnObject = new()
        {
            Id = modelObject.Id.ToString(),
            IsEnabled = true,
            NameTranslationKey = modelObject.NameTranslationKey,
            ListItemGroupFk = string.Empty
        };
        return returnObject;
    }

    public static ListItemDTO ToListItemDTO(this LanguageDTO modelObject)
    {
        ListItemDTO returnObject = new()
        {
            Id = modelObject.Id.ToString(),
            IsEnabled = true,
            NameTranslationKey = modelObject.NameTranslationKey,
            ListItemGroupFk = string.Empty
        };
        return returnObject;
    }

    public static ListItemDTO ToListItemDTO(this GeneralTypeGroupDTO modelObject)
    {
        ListItemDTO returnObject = new()
        {
            Id = modelObject.Id.ToString(),
            IsEnabled = true,
            NameTranslationKey = modelObject.NameTranslationKey,
            ListItemGroupFk = string.Empty
        };
        return returnObject;
    }

    public static ListItemDTO ToListItemDTO(this GeneralTypeItemDTO modelObject)
    {
        ListItemDTO returnObject = new()
        {
            Id = modelObject.Id.ToString(),
            IsEnabled = true,
            NameTranslationKey = modelObject.NameTranslationKey,
            ListItemGroupFk = modelObject.ListItemGroupFk.ToString()
        };
        return returnObject;
    }

    public static ListItemDTO ToListItemDTO(this EntityDTO modelObject)
    {
        ListItemDTO returnObject = new()
        {
            Id = modelObject.Id.ToString(),
            IsEnabled = true,
            NameTranslationKey = $"{modelObject.SchemaName}.{modelObject.EntityName}",
            ListItemGroupFk = string.Empty
        };
        return returnObject;
    }

    public static ListItemDTO ToListItemDTO(this MembershipTypeDTO modelObject)
    {
        ListItemDTO returnObject = new()
        {
            Id = modelObject.Id.ToString(),
            IsEnabled = true,
            NameTranslationKey = modelObject.NameTranslationKey,
            ListItemGroupFk = string.Empty
        };
        return returnObject;
    }

    public static ListItemDTO ToListItemDTO(this CompanyMembershipsDTO modelObject)
    {
        ListItemDTO returnObject = new()
        {
            Id = modelObject.Id.ToString(),
            IsEnabled = true,
            NameTranslationKey = $"{modelObject.MembershipTypeNameTranslationKey} [{modelObject.StartDate} ~ {modelObject.EndDate}]",
            ListItemGroupFk = string.Empty
        };
        return returnObject;
    }

    public static ListItemDTO ToListItemDTO(this ModuleDTO modelObject)
    {
        ListItemDTO returnObject = new()
        {
            Id = modelObject.Id.ToString(),
            IsEnabled = true,
            NameTranslationKey = modelObject.NameTranslationKey,
            ListItemGroupFk = string.Empty
        };
        return returnObject;
    }

    public static ListItemDTO ToListItemDTO(this EndpointDTO modelObject)
    {
        ListItemDTO returnObject = new()
        {
            Id = modelObject.Id.ToString(),
            IsEnabled = true,
            NameTranslationKey = modelObject.NameTranslationKey,
            ListItemGroupFk = string.Empty
        };
        return returnObject;
    }

    public static ListItemDTO ToListItemDTO(this ComponentDTO modelObject)
    {
        ListItemDTO returnObject = new()
        {
            Id = modelObject.Id.ToString(),
            IsEnabled = true,
            NameTranslationKey = modelObject.NameTranslationKey,
            ListItemGroupFk = string.Empty
        };
        return returnObject;
    }

    public static ListItemDTO ToListItemDTO(this NotificationTemplateDTO modelObject)
    {
        ListItemDTO returnObject = new()
        {
            Id = modelObject.Id.ToString(),
            IsEnabled = true,
            NameTranslationKey = $"{modelObject.Name} [{modelObject.CultureFk}]",
            ListItemGroupFk = string.Empty
        };
        return returnObject;
    }

    public static CompanyEntityAuditDTO ToCompanyEntityAuditDTO(this CompanyEntityAudit modelObject)
    {
        CompanyEntityAuditDTO returnObject = new()
        {
            EntityFk = modelObject.EntityFk,
            AuditTypeFk = modelObject.AuditTypeFk,
            CompanyFk = modelObject.CompanyFk,
            CreatedByUserFk = modelObject.CreatedByUserFk,
            CreatedDate = modelObject.CreatedDate.ToLocalTime(),
            Id = modelObject.Id,
            IsActive = modelObject.IsActive,
            LastUpdatedDate = modelObject.LastUpdatedDate.ToLocalTimeNullable(),
            LastUpdatedByUserFk = modelObject.LastUpdatedByUserFk,


            CompanyNameTranslationKey = modelObject.CompanyFkNavigation is null ? string.Empty : modelObject.CompanyFkNavigation.NameTranslationKey,
            AuditTypeNameTranslationKey = modelObject.AuditTypeFkNavigation is null ? string.Empty : modelObject.AuditTypeFkNavigation.NameTranslationKey,
            EntitySchemaName = modelObject.EntityFkNavigation is null ? string.Empty : modelObject.EntityFkNavigation.SchemaName,
            EntityName = modelObject.EntityFkNavigation is null ? string.Empty : modelObject.EntityFkNavigation.EntityName
        };
        return returnObject;
    }

    public static CompanyEntityAudit ToCompanyEntityAudit(this CompanyEntityAuditDTO modelObject)
    {
        CompanyEntityAudit returnObject = new()
        {
            EntityFk = modelObject.EntityFk,
            AuditTypeFk = modelObject.AuditTypeFk,
            CompanyFk = modelObject.CompanyFk,
            CreatedByUserFk = modelObject.CreatedByUserFk,
            CreatedDate = modelObject.CreatedDate,
            Id = modelObject.Id,
            IsActive = modelObject.IsActive,
            LastUpdatedDate = modelObject.LastUpdatedDate,
            LastUpdatedByUserFk = modelObject.LastUpdatedByUserFk
        };
        return returnObject;
    }


    public static CultureTranslation ToCultureTranslation(this CultureTranslationDTO modelObject)
    {
        CultureTranslation returnObject = new()
        {
            CultureFk = modelObject.CultureFk,
            TranslationKey = modelObject.TranslationKey,
            TranslationValue = modelObject.TranslationValue,
        };
        return returnObject;
    }

    public static List<MenuGroupDTO> ToMenuGroupDTOList(this User modelObject)
    {

        List<MenuGroupDTO> moduleList = new();
        modelObject.UserRoles.ToList().ForEach(userRoleItem =>
        {
            if (userRoleItem.RoleFkNavigation.ApplicationFkNavigation is null)
            {
                throw new Exception(TransverseExceptionEnum.UserPermissionException_ModuleFromApplicationNotFound.ToString());
            }

            userRoleItem.RoleFkNavigation.RolePermissions.DistinctBy(x => x.ModuleFk).ToList().ForEach((permission) =>
            {

                MenuGroupDTO newModuleDTO = permission.ModuleFkNavigation.ToMenuGroupDTO();
                var moduleDTO = moduleList.Where(m => m.Id == newModuleDTO.Id).FirstOrDefault();
                if (moduleDTO is null)
                {
                    if (permission.ModuleFkNavigation.IsActive)
                    {
                        moduleList.Add(newModuleDTO);
                    }
                }
                else
                {
                    newModuleDTO = moduleDTO;
                }

                if (permission.EndpointFk is null)//Have Fulla Access to all end point
                {
                    permission.ModuleFkNavigation.Endpoints.ToList().ForEach((endpoint) =>
                    {
                        MenuItemDTO endpointDTO = endpoint.ToMenuItemDTO();
                        if (endpoint.IsActive)
                        {
                            endpointDTO.CanView = true;
                            endpointDTO.CanEdit = true;
                            endpointDTO.CanCreate = true;
                            endpointDTO.CanDelete = true;
                            newModuleDTO.Items.Add(endpointDTO);
                        }
                    });
                }
                else // access to specif end point
                {
                    var endpoint = permission.ModuleFkNavigation.Endpoints.FirstOrDefault(x => x.Id == permission.EndpointFk);
                    MenuItemDTO endpointDTO = endpoint.ToMenuItemDTO();
                    if (endpoint.IsActive)
                    {
                        endpointDTO.CanView = permission.CanView ?? true;
                        endpointDTO.CanEdit = permission.CanEdit ?? true;
                        endpointDTO.CanCreate = permission.CanCreate ?? true;
                        endpointDTO.CanDelete = permission.CanDelete ?? true;

                        //if (permission.ComponentFk is not null)//access to all components
                        //{
                        //    var component = endpoint.Components.FirstOrDefault(x => x.Id == permission.ComponentFk);
                        //    MenuComponentDTO componentDTO = component.ToMenuComponentDTO();
                        //    componentDTO.RolePropertyValue = permission.PropertyValue;
                        //    if (!componentDTO.DefaultPropertyValue.Equals(componentDTO.RolePropertyValue, StringComparison.InvariantCultureIgnoreCase))
                        //    {
                        //        endpointDTO.Items.Add(componentDTO);
                        //    }
                        //}
                        newModuleDTO.Items.Add(endpointDTO);
                    }
                }
                newModuleDTO.Items = newModuleDTO.Items.DistinctBy(x => x.Id).ToList();

            });
        });
        return moduleList.OrderBy(x => x.PositionIndex).ToList();
    }

    public static MenuGroupDTO ToMenuGroupDTO(this Module modelObject)
    {
        MenuGroupDTO returnObject = new()
        {
            Id = modelObject.Id.ToString(),
            Title = modelObject.NameTranslationKey,
            Icon = modelObject.IconName,
            Description = modelObject.DescriptionTranslationKey,
            PositionIndex = modelObject.PositionIndex,
            IsVisible=modelObject.IsVisible,
            Items = new()
        };
        return returnObject;
    }

    public static MenuItemDTO ToMenuItemDTO(this Endpoint modelObject)
    {
        MenuItemDTO returnObject = new()
        {
            Id = modelObject.Id.ToString(),
            Title = modelObject.NameTranslationKey,
            Icon = modelObject.IconName,
            Description = modelObject.DescriptionTranslationKey,
            Path = modelObject.EndpointUrl,
            PositionIndex = modelObject.PositionIndex,
            IsVisible =modelObject.IsVisible,
            IsApi=modelObject.IsApi,
            Items = new(),

        };
        return returnObject;
    }

    public static MenuComponentDTO ToMenuComponentDTO(this Component modelObject)
    {
        MenuComponentDTO returnObject = new()
        {
            Id = modelObject.Id.ToString(),
            Title = modelObject.NameTranslationKey,
            Description = modelObject.DescriptionTranslationKey,
            ObjectId = modelObject.ObjectId!,
            ObjectName = modelObject.ObjectName!,
            PropertyName = modelObject.ObjectPropertyName,
            DefaultPropertyValue = modelObject.DefaultPropertyValue
        };
        return returnObject;
    }

    public static UserDTO ToUserDTO(this User modelObject)
    {
        UserDTO returnObject = new()
        {
            CompanyFk = modelObject.CompanyFk,
            Id = modelObject.Id,
            UserName = modelObject.UserName,
            Password = null!,
            EmailValidated = modelObject.EmailValidated,
            IsLocked = modelObject.IsLocked,
            RequirePasswordChange = modelObject.RequirePasswordChange,
            RetryCount = modelObject.RetryCount,
            LastAccess = modelObject.LastAccess.ToLocalTimeNullable(),
            IsActive = modelObject.IsActive,
            ActivationEmailSent = modelObject.ActivationEmailSent,
            ActivationId = modelObject.ActivationId,
            Avatar = modelObject.Avatar,
            CreatedByUserFk = modelObject.CreatedByUserFk,
            CreatedDate = modelObject.CreatedDate.ToLocalTime(),
            LastUpdatedDate = modelObject.LastUpdatedDate.ToLocalTimeNullable(),
            LockedDate = modelObject.LockedDate.ToLocalTimeNullable(),
            LastUpdatedByUserFk = modelObject.LastUpdatedByUserFk,
            Name = modelObject.Name,
            LastName = modelObject.LastName,
            Email = modelObject.Email,
            CompanyNameTranslationKey = modelObject.CompanyFkNavigation is null ? string.Empty : modelObject.CompanyFkNavigation.NameTranslationKey,
            CompanyClient = modelObject.CompanyFkNavigation is null ? string.Empty : modelObject.CompanyFkNavigation.CompanyClient,

        };
        returnObject.Roles = new();
        modelObject.UserRoles.ToList().ForEach(userRoleItem =>
        {
            if (userRoleItem.RoleFkNavigation.ApplicationFkNavigation is null)
            {
                throw new Exception(TransverseExceptionEnum.UserPermissionException_ModuleFromApplicationNotFound.ToString());
            }

            RoleDTO roleDTO = userRoleItem.RoleFkNavigation.ToRoleDTO()!;

            roleDTO.Modules = new();
            userRoleItem.RoleFkNavigation.RolePermissions.DistinctBy(x => x.ModuleFk).ToList().ForEach((permission) =>
            {
                ModuleDTO moduleDTO = new ModuleDTO();
                moduleDTO = permission.ModuleFkNavigation.ToModuleDTO();
                moduleDTO.Endpoints = new();
                permission.ModuleFkNavigation.Endpoints.ToList().ForEach((endpoint) =>
                {
                    EndpointDTO endpointDTO = new EndpointDTO();
                    endpointDTO = endpoint.ToEndpointDTO();
                    endpointDTO.Components = new();
                    endpoint.Components.ToList().ForEach((component) =>
                    {
                        endpointDTO.Components.Add(component.ToComponentDTO());
                    });
                    moduleDTO.Endpoints.Add(endpointDTO);
                });

                roleDTO.Modules.Add(moduleDTO);

            });
            returnObject.Roles.Add(roleDTO);
        });
        return returnObject;
    }

    public static UserDTOToken ToUserDTOToken(this User modelEntity, Application? applicationEntity = null, OAuthActionTypeEnum actionStatus = OAuthActionTypeEnum.OAuthActionType_Undefined)
    {
        var company = modelEntity.CompanyFkNavigation is not null ? modelEntity.CompanyFkNavigation.ToCompanyDTO() : (new Company()).ToCompanyDTO();
        var role = modelEntity.UserRoles is not null ? modelEntity.UserRoles.FirstOrDefault()!.RoleFkNavigation.ToRoleDTO() : (new Role()).ToRoleDTO();
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

            ApplicationOwnerId = applicationEntity!.OwnerFkNavigation.Id,
            ApplicationOwnerNameTranslationKey = applicationEntity.OwnerFkNavigation.NameTranslationKey,

            ApplicationId = applicationEntity.Id,
            ApplicationNameTranslationKey = applicationEntity.NameTranslationKey,

            CompanyFk = modelEntity.CompanyFk,
            CompanyNameTranslationKey = company.NameTranslationKey,

            RoleId = role?.Id,
            RoleName = role?.NameTranslationKey,
            ActionStatus = actionStatus
        };
        return returnObject;
    }


    public static User ToUser(this UserDTO modelObject)
    {
        User returnObject = new()
        {
            CompanyFk = modelObject.CompanyFk,
            Id = modelObject.Id,
            UserName = modelObject.UserName!,
            Password = null!,
            EmailValidated = modelObject.EmailValidated,
            IsLocked = modelObject.IsLocked,
            RequirePasswordChange = modelObject.RequirePasswordChange,
            RetryCount = modelObject.RetryCount,
            LastAccess = modelObject.LastAccess,
            IsActive = modelObject.IsActive,
            ActivationId = modelObject.ActivationId,
            CreatedDate = modelObject.CreatedDate,
            Email = modelObject.Email,
            Avatar = modelObject.Avatar,
            LastUpdatedDate = modelObject.LastUpdatedDate,
            LockedDate = modelObject.LockedDate,
            Name = modelObject.Name,
            LastName = modelObject.LastName,
            CreatedByUserFk = modelObject.CreatedByUserFk,
            LastUpdatedByUserFk = modelObject.LastUpdatedByUserFk,
            ActivationEmailSent = modelObject.ActivationEmailSent,

        };
        return returnObject;
    }

    public static CompanyDTO ToCompanyDTO(this Company modelObject)
    {
        CompanyDTO returnObject = new()
        {
            Id = modelObject.Id,
            NameTranslationKey = modelObject.NameTranslationKey,
            DescriptionTranslationKey = modelObject.DescriptionTranslationKey,
            CompanyClient = modelObject.CompanyClient,
            CompanySecret = modelObject.CompanySecret,
            CreationDate = modelObject.CreationDate.ToLocalTime(),
            CreatedByUserFk = modelObject.CreatedByUserFk,
            Subdomain = modelObject.Subdomain,
            Email = modelObject.Email,
            DefaultCultureFk = modelObject.DefaultCultureFk,
            CountryFk = modelObject.CountryFk,
            IsSystemCompany = modelObject.IsSystemCompany,
            Avatar = modelObject.Avatar,
            LastUpdateDate = modelObject.LastUpdateDate.ToLocalTimeNullable(),
            LastUpdateByUserFk = modelObject.LastUpdateByUserFk,
            IsActive = modelObject.IsActive,

            NameTranslationValue = modelObject.NameTranslationKey,
            DescriptionTranslationValue = modelObject.DescriptionTranslationKey,

            CountryNameTranslationKey = modelObject.CountryFkNavigation is null ? string.Empty : modelObject.CountryFkNavigation.NameTranslationKey,
            LanguageNameTranslationKey = modelObject.DefaultCultureFkNavigation is null ? string.Empty : modelObject.DefaultCultureFkNavigation.LanguageFkNavigation.NameTranslationKey
        };
        return returnObject;
    }

    public static Company ToCompany(this CompanyDTO modelObject)
    {
        Company returnObject = new()
        {
            Id = modelObject.Id,
            NameTranslationKey = modelObject.NameTranslationKey,
            DescriptionTranslationKey = modelObject.DescriptionTranslationKey,
            Subdomain = modelObject.Subdomain!,
            Email = modelObject.Email,
            CompanySecret = modelObject.CompanySecret,
            IsActive = modelObject.IsActive,
            Avatar = modelObject.Avatar,
            CountryFk = modelObject.CountryFk!,
            CreationDate = modelObject.CreationDate,
            CreatedByUserFk = modelObject.CreatedByUserFk,
            LastUpdateDate = modelObject.LastUpdateDate,
            IsSystemCompany = modelObject.IsSystemCompany,
            LastUpdateByUserFk = modelObject.LastUpdateByUserFk,

            CompanyClient = modelObject.CompanyClient,
            DefaultCultureFk = modelObject.DefaultCultureFk!,

        };
        return returnObject;
    }


    public static ApplicationDTO ToApplicationDTO(this Application modelObject)
    {
        ApplicationDTO returnObject = new()
        {
            Id = modelObject.Id,
            NameTranslationKey = modelObject.NameTranslationKey,
            DescriptionTranslationKey = modelObject.DescriptionTranslationKey,
            Avatar = modelObject.Avatar,
            IsActive = modelObject.IsActive,
            ApplicationSecret = modelObject.ApplicationSecret,
            CreatedByUserFk = modelObject.CreatedByUserFk,
            ApplicationClient = modelObject.ApplicationClient,
            CreationDate = modelObject.CreationDate.ToLocalTime(),
            LastUpdateByUserFk = modelObject.LastUpdateByUserFk,
            LastUpdateDate = modelObject.LastUpdateDate.ToLocalTimeNullable(),
            ApplicationOwnerId = modelObject.OwnerFk,
            ApplicationOwnerNameTranslationKey = modelObject.OwnerFkNavigation.NameTranslationKey,
            ApplicationOwnerDescriptionTranslationKey = modelObject.OwnerFkNavigation.DescriptionTranslationKey,

        };
        return returnObject;
    }

    public static ApplicationDTO ToApplicationDTO(this CompanyMembership modelObject)
    {
        ApplicationDTO returnObject = new()
        {
            ApplicationClient = modelObject.ApplicationFkNavigation.ApplicationClient,
            ApplicationSecret = modelObject.ApplicationFkNavigation.ApplicationSecret,
            Avatar = modelObject.ApplicationFkNavigation.Avatar,
            CreatedByUserFk = modelObject.ApplicationFkNavigation.CreatedByUserFk,
            CreationDate = modelObject.ApplicationFkNavigation.CreationDate.ToLocalTime(),
            Id = modelObject.ApplicationFkNavigation.Id,
            IsActive = modelObject.ApplicationFkNavigation.IsActive,
            LastUpdateByUserFk = modelObject.ApplicationFkNavigation.LastUpdateByUserFk,
            LastUpdateDate = modelObject.ApplicationFkNavigation.LastUpdateDate.ToLocalTimeNullable(),
            NameTranslationKey = modelObject.ApplicationFkNavigation.NameTranslationKey,
            DescriptionTranslationKey = modelObject.ApplicationFkNavigation.DescriptionTranslationKey,
            ApplicationOwnerId = modelObject.ApplicationFkNavigation.OwnerFkNavigation.Id,
            ApplicationOwnerNameTranslationKey = modelObject.ApplicationFkNavigation.OwnerFkNavigation.NameTranslationKey,
            ApplicationOwnerDescriptionTranslationKey = modelObject.ApplicationFkNavigation.OwnerFkNavigation.DescriptionTranslationKey,



        };
        return returnObject;
    }

    public static CompanyMembershipsDTO ToCompanyMembershipsDTO(this CompanyMembership modelObject)
    {
        CompanyMembershipsDTO returnObject = new()
        {
            ApplicationFk = modelObject.ApplicationFk,
            ApplicationNameTranslationKey = modelObject.ApplicationFkNavigation.NameTranslationKey,
            CompanyFk = modelObject.CompanyFk,
            CompanyNameTranslationKey = modelObject.CompanyFkNavigation.NameTranslationKey,
            CreatedByUserFk = modelObject.CreatedByUserFk,
            CreationDate = modelObject.CreationDate.ToLocalTime(),
            EndDate = modelObject.EndDate.ToLocalTimeNullable(),
            Id = modelObject.Id,
            IsActive = modelObject.IsActive,
            LastUpdateByUserFk = modelObject.LastUpdateByUserFk,
            LastUpdateDate = modelObject.LastUpdateDate.ToLocalTimeNullable(),
            MembershipTypeFk = modelObject.MembershipTypeFk,
            MembershipTypeNameTranslationKey = modelObject.MembershipTypeFkNavigation.NameTranslationKey,
            DescriptionTranslationKey = modelObject.MembershipTypeFkNavigation.DescriptionTranslationKey,
            StartDate = modelObject.StartDate.ToLocalTime(),
            ApplicationOwnerId = modelObject.ApplicationFkNavigation.OwnerFkNavigation.Id,
            ApplicationOwnerNameTranslationKey = modelObject.ApplicationFkNavigation.OwnerFkNavigation.NameTranslationKey,
        };
        return returnObject;
    }


    public static Application ToApplication(this ApplicationDTO modelObject)
    {
        Application returnObject = new()
        {
            Id = modelObject.Id,
            NameTranslationKey = modelObject.NameTranslationKey,
            DescriptionTranslationKey = modelObject.DescriptionTranslationKey,
            Avatar = modelObject.Avatar,
            IsActive = modelObject.IsActive,
            ApplicationSecret = modelObject.ApplicationSecret,
            ApplicationClient = modelObject.ApplicationClient,
            CreatedByUserFk = modelObject.CreatedByUserFk,
            CreationDate = modelObject.CreationDate,
            LastUpdateByUserFk = modelObject.LastUpdateByUserFk,
            LastUpdateDate = modelObject.LastUpdateDate,

        };
        return returnObject;
    }

    public static Role ToRole(this RoleDTO modelObject)
    {
        Role returnObject = new()
        {
            Id = modelObject.Id,
            NameTranslationKey = modelObject.NameTranslationKey,
            ApplicationFk = modelObject.ApplicationFk,
            CompanyFk = modelObject.CompanyFk,
            CreatedByUserFk = modelObject.CreatedByUserFk,
            CreationDate = modelObject.CreationDate,
            IsActive = modelObject.IsActive,
            LastUpdateByUserFk = modelObject.LastUpdateByUserFk,
            LastUpdateDate = modelObject.LastUpdateDate,
        };
        return returnObject;
    }

    public static Module ToModule(this ModuleDTO modelObject)
    {
        Module returnObject = new()
        {
            Id = modelObject.Id,
            ApplicationFk = modelObject.ApplicationFk,
            NameTranslationKey = modelObject.NameTranslationKey,
            DescriptionTranslationKey = modelObject.DescriptionTranslationKey,
            PositionIndex = modelObject.PositionIndex,
            IsActive = modelObject.IsActive,
            IsVisible = modelObject.IsVisible,
            IsApi = modelObject.IsApi,
            IconName = modelObject.IconName!,
        };
        return returnObject;
    }

    public static Endpoint ToEndpoint(this EndpointDTO modelObject)
    {
        Endpoint returnObject = new()
        {
            Id = modelObject.Id,
            ApplicationFk = modelObject.ApplicationFk,
            ModuleFk = modelObject.ModuleFk,
            NameTranslationKey = modelObject.NameTranslationKey,
            DescriptionTranslationKey = modelObject.DescriptionTranslationKey,
            PositionIndex = modelObject.PositionIndex,
            IsActive = modelObject.IsActive,
            IsVisible = modelObject.IsVisible,
            IsApi = modelObject.IsApi,
            EndpointUrl = modelObject.EndpointUrl,
            Method = modelObject.Method,
            IconName = modelObject.IconName,
        };
        return returnObject;
    }

    public static Component ToComponent(this ComponentDTO modelObject)
    {
        Component returnObject = new()
        {
            Id = modelObject.Id,
            ApplicationFk = modelObject.ApplicationFk,
            EndpointFk = modelObject.EndpointFk,
            NameTranslationKey = modelObject.NameTranslationKey,
            DescriptionTranslationKey = modelObject.DescriptionTranslationKey,
            ObjectId = modelObject.ObjectId,
            ObjectName = modelObject.ObjectName,
            ObjectPropertyName = modelObject.ObjectPropertyName,
            DefaultPropertyValue = modelObject.DefaultPropertyValue,
            PositionIndex = modelObject.PositionIndex,
        };
        return returnObject;
    }

    public static RolePermissionDTO ToRolePermissionDTO(this RolePermission modelObject)
    {
        RolePermissionDTO returnObject = new()
        {
            ApplicationFk = modelObject.ApplicationFk,
            RoleFk = modelObject.RoleFk,
            CompanyFk = modelObject.CompanyFk,
            ComponentFk = modelObject.ComponentFk,
            Id = modelObject.Id,
            ModuleFk = modelObject.ModuleFk,
            EndpointFk = modelObject.EndpointFk,
            PropertyValue = modelObject.PropertyValue,
            Obs = modelObject.Obs,
            CanView = modelObject.CanView,
            CanCreate = modelObject.CanCreate,
            CanEdit = modelObject.CanEdit,
            CanDelete = modelObject.CanDelete,
        };
        return returnObject;
    }

    public static ModuleDTO ToModuleDTO(this Module modelObject)
    {
        ModuleDTO returnObject = new()
        {
            Id = modelObject.Id,
            ApplicationFk = modelObject.ApplicationFk,
            IsActive = modelObject.IsActive,
            IsApi = modelObject.IsApi,
            IsVisible = modelObject.IsVisible,
            NameTranslationKey = modelObject.NameTranslationKey,
            DescriptionTranslationKey = modelObject.DescriptionTranslationKey,
            PositionIndex = modelObject.PositionIndex,
            ApplicationNameTranslationKey = modelObject.ApplicationFkNavigation.NameTranslationKey,
            IconName = modelObject.IconName!,


            ApplicationOwnerId = modelObject.ApplicationFkNavigation.OwnerFkNavigation.Id,
            ApplicationOwnerNameTranslationKey =
                                modelObject.ApplicationFkNavigation.OwnerFkNavigation.NameTranslationKey,
        };
        return returnObject;
    }


    public static EndpointDTO ToEndpointDTO(this Endpoint modelObject)
    {
        EndpointDTO returnObject = new()
        {
            Id = modelObject.Id,
            ApplicationFk = modelObject.ApplicationFk,
            IsActive = modelObject.IsActive,
            IsApi = modelObject.IsApi,
            IsVisible = modelObject.IsVisible,
            NameTranslationKey = modelObject.NameTranslationKey,
            DescriptionTranslationKey = modelObject.DescriptionTranslationKey,
            PositionIndex = modelObject.PositionIndex,
            ModuleFk = modelObject.ModuleFk,
            EndpointUrl = modelObject.EndpointUrl,
            Method = modelObject.Method!,
            IconName = modelObject.IconName!,
            ApplicationNameTranslationKey = modelObject.ApplicationFkNavigation is null ? string.Empty : modelObject.ApplicationFkNavigation.NameTranslationKey,
            ApplicationOwnerId = modelObject.ApplicationFkNavigation is null ? null : modelObject.ApplicationFkNavigation.OwnerFk,
            ApplicationOwnerNameTranslationKey = modelObject.ApplicationFkNavigation is null ? string.Empty : modelObject.ApplicationFkNavigation.OwnerFkNavigation.NameTranslationKey,


        };
        return returnObject;
    }

    public static ComponentDTO ToComponentDTO(this Component modelObject)
    {
        ComponentDTO returnObject = new()
        {
            EndpointFk = modelObject.EndpointFk,
            DefaultPropertyValue = modelObject.DefaultPropertyValue,
            ApplicationFk = modelObject.ApplicationFk,
            PositionIndex = modelObject.PositionIndex,
            Id = modelObject.Id,
            NameTranslationKey = modelObject.NameTranslationKey,
            DescriptionTranslationKey = modelObject.DescriptionTranslationKey,
            ObjectId = modelObject.ObjectId,
            ObjectName = modelObject.ObjectName,
            ObjectPropertyName = modelObject.ObjectPropertyName,
            ApplicationNameTranslationKey = modelObject.ApplicationFkNavigation.NameTranslationKey,
            ApplicationOwnerId = modelObject.ApplicationFkNavigation.OwnerFkNavigation.Id,
            ApplicationOwnerNameTranslationKey = modelObject.ApplicationFkNavigation.OwnerFkNavigation.NameTranslationKey,

        };
        return returnObject;
    }


    public static UserRoleDTO ToUserRoleDTO(this UserRole modelObject)
    {
        UserRoleDTO returnObject = new()
        {
            UserFk = modelObject.UserFk,
            CompanyFk = modelObject.CompanyFk,
            CompanyNameTranslationKey = modelObject.RoleFkNavigation.CompanyFkNavigation.NameTranslationKey,
            CompanyDescriptionTranslationKey = modelObject.RoleFkNavigation.CompanyFkNavigation.DescriptionTranslationKey,

            CreatedByUserFk = modelObject.CreatedByUserFk,
            CreatedDate = modelObject.CreatedDate.ToLocalTime(),
            IsActive = modelObject.IsActive,
            RoleFk = modelObject.RoleFk,
            RoleNameTranslationKey = modelObject.RoleFkNavigation.NameTranslationKey,
            RoleDescriptionTranslationKey = modelObject.RoleFkNavigation.DescriptionTranslationKey,

            UserName = modelObject.UserFkNavigation.UserName,

            ApplicationFk = modelObject.ApplicationFk,
            ApplicationNameTranslationKey = modelObject.RoleFkNavigation.ApplicationFkNavigation.NameTranslationKey,
            ApplicationDescriptionTranslationKey = modelObject.RoleFkNavigation.ApplicationFkNavigation.DescriptionTranslationKey,


            ApplicationOwnerId = modelObject.RoleFkNavigation.ApplicationFkNavigation.OwnerFk,
            ApplicationOwnerNameTranslationKey = modelObject.RoleFkNavigation.ApplicationFkNavigation.OwnerFkNavigation.NameTranslationKey,

        };
        return returnObject;
    }


    public static AuditRecordDTO ToAuditRecordDTO(this AuditRecord modelObject)
    {
        AuditRecordDTO returnObject = new()
        {
            EntityFk = modelObject.EntityFk,
            CompanyFk = modelObject.CompanyFk,
            AuditEntityIndex = modelObject.AuditEntityIndex,
            AuditTypeFk = modelObject.AuditTypeFk,
            Browser = modelObject.Browser,
            CreationDate = modelObject.CreationDate.ToLocalTime(),
            CultureFk = modelObject.CultureFk,
            DeviceType = modelObject.DeviceType,
            Engine = modelObject.Engine,
            HostName = modelObject.HostName,
            Id = modelObject.Id,
            IpAddress = modelObject.IpAddress,
            Platform = modelObject.Platform,
            UserFk = modelObject.UserFk,
            EndPointUrl = modelObject.EndPointUrl,
            Method = modelObject.Method,
            QueryString = modelObject.QueryString,
            Referer = modelObject.Referer,
            UserAgent = modelObject.UserAgent,
            ApplicationId = modelObject.ApplicationId,
            RoleId = modelObject.RoleId,
            AuditChanges = modelObject.AuditChanges,



            UserName = modelObject.UserFkNavigation is null ? string.Empty : modelObject.UserFkNavigation.UserName,
            auditEntitySchemaName = modelObject.EntityFkNavigation is null ? string.Empty : modelObject.EntityFkNavigation.SchemaName,
            AuditEntityName = modelObject.EntityFkNavigation is null ? string.Empty : modelObject.EntityFkNavigation.EntityName,
            AuditTypeNameTranslationKey = modelObject.AuditTypeFkNavigation is null ? string.Empty : modelObject.AuditTypeFkNavigation.NameTranslationKey,
            CompanyNameTranslationKey = modelObject.CompanyFkNavigation is null ? string.Empty : modelObject.CompanyFkNavigation.NameTranslationKey,
            CompanyDescriptionTranslationKey = modelObject.CompanyFkNavigation is null ? string.Empty : modelObject.CompanyFkNavigation.DescriptionTranslationKey,
        };
        return returnObject;
    }

    public static AuditRecord ToAuditRecord(this AuditRecordDTO modelObject)
    {
        AuditRecord returnObject = new()
        {
            EntityFk = modelObject.EntityFk,
            CompanyFk = modelObject.CompanyFk,
            AuditEntityIndex = modelObject.AuditEntityIndex,
            AuditTypeFk = modelObject.AuditTypeFk,
            Browser = modelObject.Browser,
            CreationDate = modelObject.CreationDate,
            CultureFk = modelObject.CultureFk,
            DeviceType = modelObject.DeviceType!,
            Engine = modelObject.Engine,
            HostName = modelObject.HostName,
            Id = modelObject.Id,
            IpAddress = modelObject.IpAddress,
            Platform = modelObject.Platform,
            UserFk = modelObject.UserFk,
            EndPointUrl = modelObject.EndPointUrl,
            Method = modelObject.Method,
            QueryString = modelObject.QueryString,
            Referer = modelObject.Referer,
            UserAgent = modelObject.UserAgent,
            ApplicationId = modelObject.ApplicationId,
            RoleId = modelObject.RoleId,
            AuditChanges = modelObject.AuditChanges,


        };
        return returnObject;
    }


    public static ActivityLogDTO ToActivityLogDTO(this ActivityLog modelObject)
    {
        ActivityLogDTO returnObject = new()
        {
            ActionTypeFk = modelObject.ActionTypeFk,
            Browser = modelObject.Browser,
            CompanyFk = modelObject.CompanyFk,
            CultureId = modelObject.CultureId,
            DeviceName = modelObject.DeviceName,
            DeviceType = modelObject.DeviceType,
            Engine = modelObject.Engine,
            EventDate = modelObject.EventDate.ToLocalTime(),
            IpAddress = modelObject.IpAddress,
            Platform = modelObject.Platform,
            TraceId = modelObject.TraceId,
            UserFk = modelObject.UserFk,
            EndPointUrl = modelObject.EndPointUrl,
            Method = modelObject.Method,
            QueryString = modelObject.QueryString,
            Referer = modelObject.Referer,
            UserAgent = modelObject.UserAgent,
            ApplicationId = modelObject.ApplicationId,
            RoleId = modelObject.RoleId,


            UserName = modelObject.UserFkNavigation is null ? string.Empty : modelObject.UserFkNavigation.UserName,
            CompanyNameTranslationKey = modelObject.UserFkNavigation is null ? string.Empty : modelObject.UserFkNavigation.CompanyFkNavigation.NameTranslationKey,
            CompanyDescriptionTranslationKey = modelObject.UserFkNavigation is null ? string.Empty : modelObject.UserFkNavigation.CompanyFkNavigation.DescriptionTranslationKey,
            ActionTypeNameTranslationKey = modelObject.ActionTypeFkNavigation is null ? string.Empty : modelObject.ActionTypeFkNavigation is null ? string.Empty : modelObject.ActionTypeFkNavigation.NameTranslationKey,
        };
        return returnObject;
    }

    public static ActivityLog ToActivityLog(this ActivityLogDTO modelObject)
    {
        ActivityLog returnObject = new()
        {
            ActionTypeFk = modelObject.ActionTypeFk,
            Browser = modelObject.Browser,
            CompanyFk = modelObject.CompanyFk,
            CultureId = modelObject.CultureId,
            DeviceName = modelObject.DeviceName,
            DeviceType = modelObject.DeviceType,
            Engine = modelObject.Engine,
            EventDate = modelObject.EventDate,
            IpAddress = modelObject.IpAddress,
            Platform = modelObject.Platform,
            TraceId = modelObject.TraceId,
            UserFk = modelObject.UserFk,
            EndPointUrl = modelObject.EndPointUrl,
            Method = modelObject.Method,
            QueryString = modelObject.QueryString,
            Referer = modelObject.Referer,
            UserAgent = modelObject.UserAgent,
            ApplicationId = modelObject.ApplicationId,
            RoleId = modelObject.RoleId,

        };
        return returnObject;
    }

    public static NotificationDTO ToNotificationDTO(this Notification modelObject)
    {
        NotificationDTO returnObject = new()
        {
            Bcc = modelObject.Bcc?.Split(commaSeparator).ToList(),
            Cc = modelObject.Cc?.Split(commaSeparator).ToList(),
            CompanyFk = modelObject.CompanyFk,
            CreationDate = modelObject.CreationDate.ToLocalTime(),
            CultureFk = modelObject.CultureFk,
            Id = modelObject.Id,
            IsHtml = modelObject.IsHtml,
            IsSent = modelObject.IsSent,
            Message = modelObject.Message,
            NotificationTemplateGroupFk = modelObject.NotificationTemplateGroupFk,
            NotificationTypeFk = modelObject.NotificationTypeFk,
            Receiver = modelObject.Receiver.Split(commaSeparator).ToList(),
            Sender = modelObject.Sender,
            SentDate = modelObject.SentDate.ToLocalTimeNullable(),
            Subject = modelObject.Subject,
            CompanyNameTranslationKey = modelObject.CompanyFkNavigation.NameTranslationKey,
            CompanyDescriptionTranslationKey = modelObject.CompanyFkNavigation.DescriptionTranslationKey,
            NotificationTypeNameTranslationKey = modelObject.NotificationTypeFkNavigation.NameTranslationKey,
            NotificationTemplateName = modelObject.NotificationTemplate.Name,
            CultureNameTranslationKey = modelObject.CultureFkNavigation.NameTranslationKey,
        };
        return returnObject;
    }

    public static Notification ToNotification(this NotificationDTO modelObject)
    {
        Notification returnObject;
        returnObject = new()
        {
            CompanyFk = modelObject.CompanyFk,
            Sender = modelObject.Sender,
            Subject = modelObject.Subject,
            Message = modelObject.Message,
            Receiver = string.Join(",", modelObject.Receiver!),
            Cc = modelObject.Cc is null ? null : string.Join(",", modelObject.Cc),
            Bcc = modelObject.Bcc is null ? null : string.Join(",", modelObject.Bcc),
            CultureFk = modelObject.CultureFk,
            NotificationTemplateGroupFk = modelObject.NotificationTemplateGroupFk,
            NotificationTypeFk = modelObject.NotificationTypeFk,
            CreationDate = modelObject.CreationDate.ToLocalTime(),
            Id = modelObject.Id,
            IsHtml = modelObject.IsHtml,
            IsSent = modelObject.IsSent,
            SentDate = modelObject.SentDate.ToLocalTimeNullable(),
        };
        return returnObject;
    }
    public static RoleDTO? ToRoleDTO(this Role? modelObject)
    {
        RoleDTO? returnObject;

        returnObject = modelObject is null ? null :
            new()
            {
                Id = modelObject!.Id,
                NameTranslationKey = modelObject!.NameTranslationKey,
                ApplicationFk = modelObject!.ApplicationFk,
                CompanyFk = modelObject!.CompanyFk,
                CreatedByUserFk = modelObject!.CreatedByUserFk,
                CreationDate = modelObject!.CreationDate.ToLocalTime(),
                IsActive = modelObject!.IsActive,
                LastUpdateByUserFk = modelObject!.LastUpdateByUserFk,
                LastUpdateDate = modelObject!.LastUpdateDate.ToLocalTimeNullable(),
                Modules = new(),
                CompanyNameTranslationKey = modelObject.CompanyFkNavigation is null ? string.Empty : modelObject.CompanyFkNavigation.NameTranslationKey,
                ApplicationNameTranslationKey = modelObject.ApplicationFkNavigation.NameTranslationKey,

                ApplicationOwnerId = modelObject.ApplicationFkNavigation.OwnerFk,
                ApplicationOwnerNameTranslationKey = modelObject.ApplicationFkNavigation.OwnerFkNavigation.NameTranslationKey,
            };

        modelObject!.RolePermissions.DistinctBy(x => x.ModuleFk).ToList().ForEach((permission) =>
        {
            ModuleDTO moduleDTO = new ModuleDTO();
            if (permission.ModuleFkNavigation is not null)
            {
                moduleDTO = permission.ModuleFkNavigation.ToModuleDTO();
                moduleDTO.Endpoints = new();
                permission.ModuleFkNavigation.Endpoints.ToList().ForEach((endpoint) =>
                {
                    EndpointDTO endpointDTO = new EndpointDTO();
                    endpointDTO = endpoint.ToEndpointDTO();
                    endpointDTO.Components = new();
                    endpoint.Components.ToList().ForEach((component) =>
                    {
                        endpointDTO.Components.Add(component.ToComponentDTO());
                    });
                    moduleDTO.Endpoints.Add(endpointDTO);
                });

                returnObject!.Modules.Add(moduleDTO);
            }

        });

        return returnObject;
    }

    public static List<NotificationTemplateDTO> ToNotificationTemplateDTO(this List<NotificationTemplate> modelObjectList)
    {
        List<NotificationTemplateDTO> returnList = [];
        modelObjectList.ForEach(modelObject =>
        {
            returnList.Add(modelObject.ToNotificationTemplateDTO()!);
        });
        return returnList;
    }

    public static NotificationTemplateDTO ToNotificationTemplateDTO(this NotificationTemplate modelObject)
    {
        NotificationTemplateDTO returnObject;
        returnObject = new()
        {
            Id = modelObject.Id,
            NotificationTemplateGroupId = modelObject.NotificationTemplateGroupId,
            CultureFk = modelObject.CultureFk,
            SubjectTemplateText = modelObject.SubjectTemplateText,
            MessageTemplateText = modelObject.MessageTemplateText,
            IsHtml = modelObject.IsHtml,
            Name = modelObject.Name,
            CultureNameTranslationKey = modelObject.CultureFkNavigation?.NameTranslationKey ?? string.Empty
        };
        return returnObject;
    }

    public static NotificationTemplate ToNotificationTemplate(this NotificationTemplateDTO modelObject)
    {
        NotificationTemplate returnObject = new()
        {
            Id = modelObject.Id,
            NotificationTemplateGroupId = modelObject.NotificationTemplateGroupId,
            CultureFk = modelObject.CultureFk,
            SubjectTemplateText = modelObject.SubjectTemplateText,
            MessageTemplateText = modelObject.MessageTemplateText,
            IsHtml = modelObject.IsHtml,
            Name = modelObject.Name
        };
        return returnObject;
    }


    public static PictureDTO ToPictureDTO(this Picture modelObject)
    {
        PictureDTO returnObject = new()
        {
            CompanyFk = modelObject.CompanyFk,
            BinaryPicture = modelObject.BinaryPicture!,
            CreatedByUserFk = modelObject.CreatedByUserFk,
            CreationDate = modelObject.CreationDate.ToLocalTime(),
            EntityFk = modelObject.EntityFk,
            FileTypeFk = modelObject.FileTypeFk,
            Id = modelObject.Id,
            IsActive = modelObject.IsActive,
            LastUpdateByUserFk = modelObject.LastUpdateByUserFk,
            LastUpdateDate = modelObject.LastUpdateDate!.ToLocalTimeNullable(),
            Name = modelObject.Name,
            PictureCategoryFk = modelObject.PictureCategoryFk,
            PictureSize = modelObject.PictureSize,


            PictureCategoryNameTranslationKey = modelObject.PictureCategoryFkNavigation.NameTranslationKey,
            FileTypeNameTranslationKey = modelObject.FileTypeFkNavigation.NameTranslationKey,

            CompanyId = modelObject.CompanyFkNavigation.Id,
            CompanyNameTranslationKey = modelObject.CompanyFkNavigation.NameTranslationKey,

            EntitySchemaName = modelObject.EntityFkNavigation.SchemaName,
            EntityName = modelObject.EntityFkNavigation.EntityName
        };
        return returnObject;
    }

    public static Picture ToPicture(this PictureDTO modelObject)
    {
        Picture returnObject = new()
        {
            CompanyFk = modelObject.CompanyFk,
            BinaryPicture = modelObject.BinaryPicture,
            CreatedByUserFk = modelObject.CreatedByUserFk,
            CreationDate = modelObject.CreationDate,
            EntityFk = modelObject.EntityFk,
            FileTypeFk = modelObject.FileTypeFk,
            Id = modelObject.Id,
            IsActive = modelObject.IsActive,
            LastUpdateByUserFk = modelObject.LastUpdateByUserFk,
            LastUpdateDate = modelObject.LastUpdateDate,
            Name = modelObject.Name,
            PictureCategoryFk = modelObject.PictureCategoryFk,
            PictureSize = modelObject.PictureSize,
        };
        return returnObject;
    }

    public static EntityDTO ToEntityDTO(this Entity modelObject)
    {
        EntityDTO returnObject = new()
        {
            EntityName = modelObject.EntityName,
            Id = modelObject.Id,
            IsActive = modelObject.IsActive,
            IsSystemEntity = modelObject.IsSystemEntity,
            SchemaName = modelObject.SchemaName,
        };
        return returnObject;
    }

    public static Entity ToEntity(this EntityDTO modelObject)
    {
        Entity returnObject = new()
        {
            EntityName = modelObject.EntityName,
            Id = modelObject.Id,
            IsActive = modelObject.IsActive,
            IsSystemEntity = modelObject.IsSystemEntity,
            SchemaName = modelObject.SchemaName,
        };
        return returnObject;
    }

    public static MembershipTypeDTO ToMembershipTypeDTO(this MembershipType modelObject)
    {
        MembershipTypeDTO returnObject = new()
        {
            Id = modelObject.Id,
            NameTranslationKey = modelObject.NameTranslationKey,
            DescriptionTranslationKey = modelObject.DescriptionTranslationKey,
            CreationDate = modelObject.CreationDate.ToLocalTime(),
            CreatedByUserFk = modelObject.CreatedByUserFk,
            StartDate = modelObject.StartDate.ToLocalTime(),
            EndDate = modelObject.EndDate.ToLocalTimeNullable(),
            LastUpdateDate = modelObject.LastUpdateDate.ToLocalTimeNullable(),
            LastUpdateByUserFk = modelObject.LastUpdateByUserFk,
            IsActive = modelObject.IsActive
        };
        return returnObject;
    }

    public static MembershipType ToMembershipType(this MembershipTypeDTO modelObject)
    {
        MembershipType returnObject = new()
        {
            Id = modelObject.Id,
            NameTranslationKey = modelObject.NameTranslationKey,
            DescriptionTranslationKey = modelObject.DescriptionTranslationKey,
            CreationDate = modelObject.CreationDate,
            CreatedByUserFk = modelObject.CreatedByUserFk,
            StartDate = modelObject.StartDate,
            EndDate = modelObject.EndDate,
            LastUpdateDate = modelObject.LastUpdateDate,
            LastUpdateByUserFk = modelObject.LastUpdateByUserFk,
            IsActive = modelObject.IsActive
        };
        return returnObject;
    }

    public static SequencesDTO ToSecuencesDTO(this Sequence modelObject)
    {
        SequencesDTO returnObject = new()
        {
            Id = modelObject.Id,
            CompanyFk = modelObject.CompanyFk,
            ApplicationFk = modelObject.ApplicationFk,
            SequenceTypeFk = modelObject.SequenceTypeFk,
            SequenceNameFormat = modelObject.SequenceNameFormat,
            SequenceIndex = modelObject.SequenceIndex,
            TextFormat = modelObject.TextFormat,

            CompanyNameTranslationKey = modelObject.CompanyFkNavigation?.NameTranslationKey ?? string.Empty,
            SequenceTypeNameTranslationKey = modelObject.SequenceTypeFkNavigation?.NameTranslationKey ?? string.Empty,
            ApplicationNameTranslationKey = modelObject.ApplicationFkNavigation?.NameTranslationKey ?? string.Empty,
        };
        return returnObject;
    }

    public static Sequence ToSequence(this SequencesDTO modelObject)
    {
        Sequence returnObject = new()
        {
            Id = modelObject.Id,
            CompanyFk = modelObject.CompanyFk,
            ApplicationFk = modelObject.ApplicationFk,
            SequenceTypeFk = modelObject.SequenceTypeFk,
            SequenceNameFormat = modelObject.SequenceNameFormat,
            SequenceIndex = modelObject.SequenceIndex,
            TextFormat = modelObject.TextFormat,
        };
        return returnObject;
    }

    public static GeneralTypeItemDTO ToGeneralTypeItemDTO(this GeneralTypeItem modelObject)
    {
        GeneralTypeItemDTO returnObject = new()
        {
            Id = modelObject.Id,
            NameTranslationKey = modelObject.NameTranslationKey,
            ListItemGroupFk = modelObject.ListItemGroupFk,
            OrderIndex = modelObject.OrderIndex,
            IsEnabled = modelObject.IsEnabled,
        };
        return returnObject;
    }

    public static GeneralTypeItem ToGeneralTypeItem(this GeneralTypeItemDTO modelObject)
    {
        GeneralTypeItem returnObject = new()
        {
            Id = modelObject.Id,
            NameTranslationKey = modelObject.NameTranslationKey,
            ListItemGroupFk = modelObject.ListItemGroupFk,
            OrderIndex = modelObject.OrderIndex,
            IsEnabled = modelObject.IsEnabled
        };
        return returnObject;
    }

    public static GeneralTypeGroupDTO ToGeneralTypeGroupDTO(this GeneralTypeGroup modelObject)
    {
        GeneralTypeGroupDTO returnObject = new()
        {
            Id = modelObject.Id,
            NameTranslationKey = modelObject.NameTranslationKey,
            IsSystemType = modelObject.IsSystemType
        };
        return returnObject;
    }

    public static GeneralTypeGroup ToGeneralTypeGroup(this GeneralTypeGroupDTO modelObject)
    {
        GeneralTypeGroup returnObject = new()
        {
            Id = modelObject.Id,
            NameTranslationKey = modelObject.NameTranslationKey,
            IsSystemType = modelObject.IsSystemType
        };
        return returnObject;
    }

}
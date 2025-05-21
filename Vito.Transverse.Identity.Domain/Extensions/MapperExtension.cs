using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Transverse.Identity.Domain.Enums;
using Vito.Transverse.Identity.Domain.Models;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.Domain.Extensions;

public static class MapperExtension
{
    private const string commaSeparator = ",";
    public static List<PersonDTO> ToPersonDTOList(this List<Person> modelObjectList)
    {
        List<PersonDTO> returnList = [];
        modelObjectList.ForEach(modelObject =>
        {
            returnList.Add(modelObject.ToPersonDTO());
        });
        return returnList;
    }

    public static PersonDTO ToPersonDTO(this Person modelObject)
    {
        PersonDTO returnObject = new()
        {
            CompanyFk = modelObject.CompanyFk,
            Id = modelObject.Id,
            DocumentTypeFk = modelObject.DocumentTypeFk,
            DocumentValue = modelObject.DocumentValue,
            Name = modelObject.Name,
            LastName = modelObject.LastName,
            Email = modelObject.Email,
            GenderFk = modelObject.GenderFk,
            MobileNumber = modelObject.MobileNumber,
            Avatar = modelObject.Avatar
        };
        return returnObject;
    }

    public static Person ToPerson(this PersonDTO modelObject)
    {
        Person returnObject = new()
        {
            CompanyFk = modelObject.CompanyFk!.Value,
            Id = modelObject.Id!.Value,
            DocumentTypeFk = modelObject.DocumentTypeFk!.Value,
            DocumentValue = modelObject.DocumentValue!,
            Name = modelObject.Name!,
            LastName = modelObject.LastName!,
            Email = modelObject.Email!,
            GenderFk = modelObject.GenderFk!.Value,
            MobileNumber = modelObject.MobileNumber,
            Avatar = modelObject.Avatar
        };
        return returnObject;
    }

    public static List<CultureDTO> ToCultureDTOList(this List<Culture> modelObjectList)
    {
        List<CultureDTO> returnList = [];
        modelObjectList.ForEach(modelObject =>
        {
            returnList.Add(modelObject.ToCultureDTO());
        });
        return returnList;
    }

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

    public static List<CultureTranslationDTO> ToCultureTranslationDTOList(this List<CultureTranslation> modelObjectList)
    {
        List<CultureTranslationDTO> returnList = [];
        modelObjectList.ForEach(modelObject =>
        {
            returnList.Add(modelObject.ToCultureTranslationDTO());
        });
        return returnList;
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

    public static List<ListItemDTO> ToListItemDTOList(this List<GeneralTypeItem> modelObjectList)
    {
        List<ListItemDTO> returnList = [];
        modelObjectList.ForEach(modelObject =>
        {
            returnList.Add(modelObject.ToListItemDTO());
        });
        return returnList;
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

    public static List<ListItemDTO> ToListItemDTOList(this List<CultureDTO> modelObjectList)
    {
        List<ListItemDTO> returnList = [];
        modelObjectList.ForEach(modelObject =>
        {
            returnList.Add(modelObject.ToListItemDTO());
        });
        return returnList;
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



    public static List<CompanyEntityAuditDTO> ToCompanyEntityAuditDTOList(this List<CompanyEntityAudit> modelObjectList)
    {
        List<CompanyEntityAuditDTO> returnList = [];
        modelObjectList.ForEach(modelObject =>
        {
            returnList.Add(modelObject.ToCompanyEntityAuditDTO());
        });
        return returnList;
    }

    public static CompanyEntityAuditDTO ToCompanyEntityAuditDTO(this CompanyEntityAudit modelObject)
    {
        CompanyEntityAuditDTO returnObject = new()
        {
            EntityFk = modelObject.EntityFk,
            AuditTypeFk = modelObject.AuditTypeFk,
            CompanyFk = modelObject.CompanyFk,
            CreatedByUserFk = modelObject.CreatedByUserFk,
            CreationDate = modelObject.CreationDate.ToLocalTime(),
            Id = modelObject.Id,
            IsActive = modelObject.IsActive,
            LastUpdateDate = modelObject.LastUpdateDate.ToLocalTimeNullable(),
            UpdatedByUserFk = modelObject.UpdatedByUserFk,
            CompanyNameTranslationKey = modelObject.CompanyFkNavigation.NameTranslationKey,
            AuditTypeNameTranslationKey = modelObject.AuditTypeFkNavigation.NameTranslationKey,
            EntitySchemaName = modelObject.EntityFkNavigation.SchemaName,
            EntityName = modelObject.EntityFkNavigation.EntityName
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

    public static List<UserDTO> ToUserDTOList(this List<User> modelObjectList)
    {
        List<UserDTO> returnList = [];
        modelObjectList.ForEach(modelObject =>
        {
            returnList.Add(modelObject.ToUserDTO());
        });
        return returnList;
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
            CreationDate = modelObject.CreationDate.ToLocalTimeNullable(),
            LastUpdateDate = modelObject.LastUpdateDate.ToLocalTimeNullable(),
            LockedDate = modelObject.LockedDate.ToLocalTimeNullable(),
            UpdatedByUserFk = modelObject.UpdatedByUserFk,
            Name = modelObject.Name,
            LastName = modelObject.LastName,
            Email = modelObject.Email,
            CompanyNameTranslationKey = modelObject.CompanyFkNavigation.NameTranslationKey,

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

            ApplicationOwnerId = applicationEntity!.ApplicationOwners.First().CompanyFkNavigation.Id,
            ApplicationOwnerNameTranslationKey = applicationEntity.ApplicationOwners.First().CompanyFkNavigation.NameTranslationKey,

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
            LastAccess = modelObject.LastAccess.ToLocalTimeNullable(),
            IsActive = modelObject.IsActive,
            ActivationId = modelObject.ActivationId,
            CreationDate = modelObject.CreationDate.ToLocalTimeNullable(),
            Email = modelObject.Email,
            Avatar = modelObject.Avatar,
            LastUpdateDate = modelObject.LastUpdateDate.ToLocalTimeNullable(),
            LockedDate = modelObject.LockedDate.ToLocalTimeNullable(),
            Name = modelObject.Name,
            LastName = modelObject.LastName,
            CreatedByUserFk = modelObject.CreatedByUserFk,
            UpdatedByUserFk = modelObject.UpdatedByUserFk,
            ActivationEmailSent = modelObject.ActivationEmailSent,

        };
        return returnObject;
    }

    public static List<CompanyDTO> ToCompanyDTOList(this List<Company> modelObjectList)
    {
        List<CompanyDTO> returnList = [];
        modelObjectList.ForEach(modelObject =>
        {
            returnList.Add(modelObject.ToCompanyDTO());
        });
        return returnList;
    }

    public static CompanyDTO ToCompanyDTO(this Company modelObject)
    {
        CompanyDTO returnObject = new(modelObject.Id,
            modelObject.NameTranslationKey,
            modelObject.DescriptionTranslationKey,
            modelObject.CompanyClient,
            modelObject.CompanySecret,
            modelObject.CreationDate.ToLocalTime(),
            modelObject.CreatedByUserFk,
            modelObject.Subdomain,
            modelObject.Email,
            modelObject.DefaultCultureFk,
            modelObject.CountryFk,
            modelObject.IsSystemCompany,
            modelObject.Avatar,
            modelObject.LastUpdateDate.ToLocalTimeNullable(),
            modelObject.LastUpdateByUserFk,
            modelObject.IsActive,
            modelObject.CountryFkNavigation is null ? string.Empty : modelObject.CountryFkNavigation.NameTranslationKey,
            modelObject.DefaultCultureFkNavigation is null ? string.Empty : modelObject.DefaultCultureFkNavigation.LanguageFkNavigation.NameTranslationKey
            );
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
            CreationDate = modelObject.CreationDate.ToLocalTime(),
            CreatedByUserFk = modelObject.CreatedByUserFk,
            LastUpdateDate = modelObject.LastUpdateDate.ToLocalTimeNullable(),
            IsSystemCompany = modelObject.IsSystemCompany,
            LastUpdateByUserFk = modelObject.LastUpdateByUserFk,

        };
        return returnObject;
    }



    public static List<ApplicationDTO> ToApplicationDTOList(this List<Application> modelObjectList)
    {
        List<ApplicationDTO> returnList = [];
        modelObjectList.ForEach(modelObject =>
        {
            returnList.Add(modelObject.ToApplicationDTO());
        });
        return returnList;
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
            ApplicationOwnerId = modelObject.ApplicationOwners.First().CompanyFk,
            ApplicationOwnerNameTranslationKey = modelObject.ApplicationOwners.First().CompanyFkNavigation.NameTranslationKey,
            ApplicationOwnerDescriptionTranslationKey = modelObject.ApplicationOwners.First().CompanyFkNavigation.DescriptionTranslationKey,

        };
        return returnObject;
    }


    public static List<ApplicationDTO> ToApplicationDTOList(this List<CompanyMembership> modelObjectList)
    {
        List<ApplicationDTO> returnList = [];
        modelObjectList.ForEach(modelObject =>
        {
            returnList.Add(modelObject.ToApplicationDTO());
        });
        return returnList;
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
            ApplicationOwnerId = modelObject.ApplicationFkNavigation.ApplicationOwners.First().CompanyFkNavigation.Id,
            ApplicationOwnerNameTranslationKey = modelObject.ApplicationFkNavigation.ApplicationOwners.First().CompanyFkNavigation.NameTranslationKey,
            ApplicationOwnerDescriptionTranslationKey = modelObject.ApplicationFkNavigation.ApplicationOwners.First().CompanyFkNavigation.DescriptionTranslationKey,

            CompanyId = modelObject.CompanyFkNavigation.Id,
            CompanyNameTranslationKey = modelObject.CompanyFkNavigation.NameTranslationKey,

        };
        return returnObject;
    }

    public static List<CompanyMembershipsDTO> ToCompanyMembershipsDTOList(this List<CompanyMembership> modelObjectList)
    {
        List<CompanyMembershipsDTO> returnList = [];
        modelObjectList.ForEach(modelObject =>
        {
            returnList.Add(modelObject.ToCompanyMembershipsDTO());
        });
        return returnList;
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
            ApplicationOwnerId = modelObject.ApplicationFkNavigation.ApplicationOwners.First().CompanyFkNavigation.Id,
            ApplicationOwnerNameTranslationKey = modelObject.ApplicationFkNavigation.ApplicationOwners.First().CompanyFkNavigation.NameTranslationKey,
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
        };
        return returnObject;
    }

    public static List<RoleDTO> ToRoleDTOList(this List<Role> modelObjectList)
    {
        List<RoleDTO> returnList = [];
        modelObjectList.ForEach(modelObject =>
        {
            returnList.Add(modelObject.ToRoleDTO()!);
        });
        return returnList;
    }

    public static List<RolePermissionDTO> ToRolePermissionsDTOList(this List<RolePermission> modelObjectList)
    {
        List<RolePermissionDTO> returnList = [];
        modelObjectList.ForEach(modelObject =>
        {
            returnList.Add(modelObject.ToRolePermissionDTO()!);
        });
        return returnList;
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
            PropertyValue = modelObject.PropertyValue
        };
        return returnObject;
    }
    public static List<ModuleDTO> ToModuleDTOList(this List<Module> modelObjectList)
    {
        List<ModuleDTO> returnList = [];
        modelObjectList.ForEach(modelObject =>
        {
            returnList.Add(modelObject.ToModuleDTO()!);
        });
        return returnList;
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


            ApplicationOwnerId = modelObject.ApplicationFkNavigation.ApplicationOwners.Count == 0 ? (long)Decimal.Zero :
                                modelObject.ApplicationFkNavigation.ApplicationOwners.First().CompanyFkNavigation.Id,
            ApplicationOwnerNameTranslationKey = modelObject.ApplicationFkNavigation.ApplicationOwners.Count == 0 ? string.Empty :
                                modelObject.ApplicationFkNavigation.ApplicationOwners.First().CompanyFkNavigation.NameTranslationKey,
        };
        return returnObject;
    }

    public static List<EndpointDTO> ToEndpointDTOList(this List<Endpoint> modelObjectList)
    {
        List<EndpointDTO> returnList = [];
        modelObjectList.ForEach(modelObject =>
        {
            returnList.Add(modelObject.ToEndpointDTO()!);
        });
        return returnList;
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
            ApplicationNameTranslationKey = modelObject.ApplicationFkNavigation is null ? string.Empty : modelObject.ApplicationFkNavigation.NameTranslationKey,
            ApplicationOwnerId = modelObject.ApplicationFkNavigation is null || modelObject.ApplicationFkNavigation.ApplicationOwners.Count == 0 ?
                                    (long)decimal.Zero :
                                    modelObject.ApplicationFkNavigation.ApplicationOwners.First().CompanyFkNavigation.Id,
            ApplicationOwnerNameTranslationKey = modelObject.ApplicationFkNavigation is null || modelObject.ApplicationFkNavigation.ApplicationOwners.Count == 0 ?
                                    string.Empty :
                                    modelObject.ApplicationFkNavigation.ApplicationOwners.First().CompanyFkNavigation.NameTranslationKey,


        };
        return returnObject;
    }

    public static List<ComponentDTO> ToComponentDTOList(this List<Component> modelObjectList)
    {
        List<ComponentDTO> returnList = [];
        modelObjectList.ForEach(modelObject =>
        {
            returnList.Add(modelObject.ToComponentDTO()!);
        });
        return returnList;
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
            ApplicationOwnerId = modelObject.ApplicationFkNavigation.ApplicationOwners.First().CompanyFkNavigation.Id,
            ApplicationOwnerNameTranslationKey = modelObject.ApplicationFkNavigation.ApplicationOwners.First().CompanyFkNavigation.NameTranslationKey,

        };
        return returnObject;
    }


    public static List<UserRoleDTO> ToUserRoleDTOList(this List<UserRole> modelObjectList)
    {
        List<UserRoleDTO> returnList = [];
        modelObjectList.ForEach(modelObject =>
        {
            returnList.Add(modelObject.ToUserRoleDTO()!);
        });
        return returnList;
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

            ApplicationFk = modelObject.ApplicationFk,
            ApplicationNameTranslationKey = modelObject.RoleFkNavigation.ApplicationFkNavigation.NameTranslationKey,
            ApplicationDescriptionTranslationKey = modelObject.RoleFkNavigation.ApplicationFkNavigation.DescriptionTranslationKey,


            ApplicationOwnerId = modelObject.RoleFkNavigation.ApplicationFkNavigation.ApplicationOwners.First().CompanyFkNavigation.Id,
            ApplicationOwnerNameTranslationKey = modelObject.RoleFkNavigation.ApplicationFkNavigation.ApplicationOwners.First().CompanyFkNavigation.NameTranslationKey,

        };
        return returnObject;
    }





    public static List<AuditRecordDTO> ToAuditRecordDTOList(this List<AuditRecord> modelObjectList)
    {
        List<AuditRecordDTO> returnList = [];
        modelObjectList.ForEach(modelObject =>
        {
            returnList.Add(modelObject.ToAuditRecordDTO()!);
        });
        return returnList;
    }

    public static AuditRecordDTO ToAuditRecordDTO(this AuditRecord modelObject)
    {
        AuditRecordDTO returnObject = new()
        {
            AuditEntityFk = modelObject.EntityFk,
            AuditInfoJson = modelObject.AuditInfoJson,
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
            auditEntitySchemaName = modelObject.EntityFkNavigation.SchemaName,
            AuditEntityName = modelObject.EntityFkNavigation.EntityName,
            AuditTypeNameTranslationKey = modelObject.AuditTypeFkNavigation.NameTranslationKey,
            UserName = modelObject.UserFkNavigation.UserName,
            EndPointUrl = modelObject.EndPointUrl,
            Method = modelObject.Method,
            JwtToken = modelObject.JwtToken,

            CompanyNameTranslationKey = modelObject.CompanyFkNavigation.NameTranslationKey,
            CompanyDescriptionTranslationKey = modelObject.CompanyFkNavigation.DescriptionTranslationKey,


        };
        return returnObject;
    }




    public static List<ActivityLogDTO> ToActivityLogDTOList(this List<ActivityLog> modelObjectList)
    {
        List<ActivityLogDTO> returnList = [];
        modelObjectList.ForEach(modelObject =>
        {
            returnList.Add(modelObject.ToActivityLogDTO()!);
        });
        return returnList;
    }

    public static ActivityLogDTO ToActivityLogDTO(this ActivityLog modelObject)
    {
        ActivityLogDTO returnObject = new()
        {
            ActionTypeFk = modelObject.ActionTypeFk,
            AddtionalInformation = modelObject.AddtionalInformation,
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
            JwtToken = modelObject.JwtToken,

            UserName = modelObject.UserFkNavigation.UserName,
            CompanyNameTranslationKey = modelObject.UserFkNavigation.CompanyFkNavigation.NameTranslationKey,
            CompanyDescriptionTranslationKey = modelObject.UserFkNavigation.CompanyFkNavigation.DescriptionTranslationKey,
            ActionTypeNameTranslationKey = modelObject.ActionTypeFkNavigation.NameTranslationKey,
        };
        return returnObject;
    }



    public static List<NotificationDTO> ToNotificationDTOList(this List<Notification> modelObjectList)
    {
        List<NotificationDTO> returnList = [];
        modelObjectList.ForEach(modelObject =>
        {
            returnList.Add(modelObject.ToNotificationDTO()!);
        });
        return returnList;
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

                ApplicationOwnerId = modelObject.ApplicationFkNavigation.ApplicationOwners.Count == 0 ? (long)Decimal.Zero
                                    : modelObject.ApplicationFkNavigation.ApplicationOwners.First().CompanyFkNavigation.Id,
                ApplicationOwnerNameTranslationKey = modelObject.ApplicationFkNavigation.ApplicationOwners.Count == 0 ? string.Empty :
                modelObject.ApplicationFkNavigation.ApplicationOwners.First().CompanyFkNavigation.NameTranslationKey,
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

    public static NotificationTemplateDTO ToNotificationTemplateDTO(this NotificationTemplate modelObject)
    {
        NotificationTemplateDTO returnObject;
        returnObject = new()
        {
            Id = modelObject.Id,
            CultureFk = modelObject.CultureFk,
            SubjectTemplateText = modelObject.SubjectTemplateText,
            MessageTemplateText = modelObject.MessageTemplateText,
            IsHtml = modelObject.IsHtml,
            Name = modelObject.Name,
        };
        return returnObject;
    }


    public static List<PictureDTO> ToPictureDTOList(this List<Picture> modelObjectList)
    {
        List<PictureDTO> returnList = [];
        modelObjectList.ForEach(modelObject =>
        {
            returnList.Add(modelObject.ToPictureDTO()!);
        });
        return returnList;
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
            CreationDate = modelObject.CreationDate.ToLocalTime(),
            EntityFk = modelObject.EntityFk,
            FileTypeFk = modelObject.FileTypeFk,
            Id = modelObject.Id,
            IsActive = modelObject.IsActive,
            LastUpdateByUserFk = modelObject.LastUpdateByUserFk,
            LastUpdateDate = modelObject.LastUpdateDate.ToLocalTimeNullable(),
            Name = modelObject.Name,
            PictureCategoryFk = modelObject.PictureCategoryFk,
            PictureSize = modelObject.PictureSize,
        };
        return returnObject;
    }

    public static DateTime? ToLocalTimeNullable(this DateTime? utcTime)
    {
        DateTime? localTime = null!;
        if (utcTime is null)
        {
            localTime = null!;
        }
        else
        {
            localTime = utcTime.Value.ToLocalTime();
        }
        return localTime;
    }

}
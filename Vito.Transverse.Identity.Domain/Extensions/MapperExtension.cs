using System.Security;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Models.SocialNetworks;
using Vito.Transverse.Identity.Domain.Models;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.Domain.Extensions;

public static class MapperExtension
{
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
            AuditEntityFk = modelObject.AuditEntityFk,
            AuditTypeFk = modelObject.AuditTypeFk,
            CompanyFk = modelObject.CompanyFk,
            CreatedByUserFk = modelObject.CreatedByUserFk,
            CreationDate = modelObject.CreationDate,
            Id = modelObject.Id,
            IsActive = modelObject.IsActive,
            LastUpdateDate = modelObject.LastUpdateDate,
            UpdatedByUserFk = modelObject.UpdatedByUserFk,
            CompanyNameTranslationKey = modelObject.CompanyFkNavigation.NameTranslationKey,
            AuditTypeNameTranslationKey = modelObject.AuditTypeFkNavigation.NameTranslationKey,
            AuditEntitySchemaName = modelObject.AuditEntityFkNavigation.SchemaName,
            AuditEntityName = modelObject.AuditEntityFkNavigation.EntityName
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
            LastAccess = modelObject.LastAccess,
            IsActive = modelObject.IsActive,
            ActivationEmailSent = modelObject.ActivationEmailSent,
            ActivationId = modelObject.ActivationId,
            Avatar = modelObject.Avatar,
            CreatedByUserFk = modelObject.CreatedByUserFk,
            CreationDate = modelObject.CreationDate,
            LastUpdateDate = modelObject.LastUpdateDate,
            LockedDate = modelObject.LockedDate,
            UpdatedByUserFk = modelObject.UpdatedByUserFk,
            Name = modelObject.Name,
            LastName = modelObject.LastName,
            Email = modelObject.Email,
            CompanyNameTranslationKey = modelObject.CompanyFkNavigation.NameTranslationKey

        };
        returnObject.Roles = new();
        modelObject.UserRoles.ToList().ForEach(userRoleItem =>
        {
            RoleDTO roleDTO = userRoleItem.RoleFkNavigation.ToRoleDTO()!;
            roleDTO.Modules = new();
            userRoleItem.RoleFkNavigation.RolePermissions.DistinctBy(x => x.ModuleFk).ToList().ForEach((permission) =>
            {
                ModuleDTO moduleDTO = new ModuleDTO();
                moduleDTO = permission.ModuleFkNavigation.ToModuleDTO();
                moduleDTO.Pages = new();
                permission.ModuleFkNavigation.Pages.ToList().ForEach((page) =>
                {
                    PageDTO pageDTO = new PageDTO();
                    pageDTO = page.ToPageDTO();
                    pageDTO.Components = new();
                    page.Components.ToList().ForEach((component) =>
                    {
                        pageDTO.Components.Add(component.ToComponentDTO());
                    });
                    moduleDTO.Pages.Add(pageDTO);
                });

                roleDTO.Modules.Add(moduleDTO);

            });
            returnObject.Roles.Add(roleDTO);
        });
        return returnObject;
    }

    public static UserDTOToken ToUserDTOToken(this User modelObject, long? applicationId = null, string? applicationName = null, OAuthActionTypeEnum actionStatus = OAuthActionTypeEnum.OAuthActionType_Undefined)
    {
        //var person = modelObject.PersonFkNavigation is not null ? modelObject.PersonFkNavigation.ToPersonDTO() : (new Person()).ToPersonDTO();
        var company = modelObject.CompanyFkNavigation is not null ? modelObject.CompanyFkNavigation.ToCompanyDTO() : (new Company()).ToCompanyDTO();
        var role = modelObject.UserRoles is not null ? modelObject.UserRoles.FirstOrDefault()!.RoleFkNavigation.ToRoleDTO() : (new Role()).ToRoleDTO();
        UserDTOToken returnObject = new()
        {
            CompanyFk = modelObject.CompanyFk,
            Id = modelObject.Id,
            UserName = modelObject.UserName,
            Password = null!,
            EmailValidated = modelObject.EmailValidated,
            IsLocked = modelObject.IsLocked,
            RequirePasswordChange = modelObject.RequirePasswordChange,
            RetryCount = modelObject.RetryCount,
            LastAccess = modelObject.LastAccess,
            IsActive = modelObject.IsActive,

            Name = modelObject.Name,
            LastName = modelObject.LastName,
            Email = modelObject.Email,



            ApplicationId = applicationId,
            ApplicationName = applicationName,
            CompanyName = company.NameTranslationKey,
            RoleName = role?.NameTranslationKey,
            RoleId = role?.Id,
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
            ActivationId = modelObject.ActivationId
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
            modelObject.CompanyClient,
            modelObject.CompanySecret,
            modelObject.CreationDate,
            modelObject.CreatedByUserFk,
            modelObject.Subdomain,
            modelObject.Email,
            modelObject.DefaultCultureFk,
            modelObject.CountryFk,
            modelObject.IsSystemCompany,
            modelObject.Avatar, modelObject.LastUpdateDate,
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
            Subdomain = modelObject.Subdomain!,
            Email = modelObject.Email,
            CompanySecret = modelObject.CompanySecret,
            IsActive = modelObject.IsActive,
            Avatar = modelObject.Avatar,
            CountryFk = modelObject.CountryFk!
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
            Avatar = modelObject.Avatar,
            IsActive = modelObject.IsActive,
            ApplicationSecret = modelObject.ApplicationSecret,
            CreatedByUserFk = modelObject.CreatedByUserFk,
            ApplicationClient = modelObject.ApplicationClient,
            CreationDate = modelObject.CreationDate,
            LastUpdateByUserFk = modelObject.LastUpdateByUserFk,
            LastUpdateDate = modelObject.LastUpdateDate,
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
            CreationDate = modelObject.ApplicationFkNavigation.CreationDate,
            Id = modelObject.ApplicationFkNavigation.Id,
            IsActive = modelObject.ApplicationFkNavigation.IsActive,
            LastUpdateByUserFk = modelObject.ApplicationFkNavigation.LastUpdateByUserFk,
            LastUpdateDate = modelObject.ApplicationFkNavigation.LastUpdateDate,
            NameTranslationKey = modelObject.ApplicationFkNavigation.NameTranslationKey,

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
            CreationDate = modelObject.CreationDate,
            EndDate = modelObject.EndDate,
            Id = modelObject.Id,
            IsActive = modelObject.IsActive,
            LastUpdateByUserFk = modelObject.LastUpdateByUserFk,
            LastUpdateDate = modelObject.LastUpdateDate,
            MembershipTypeFk = modelObject.MembershipTypeFk,
            MembershipTypeNameTranslationKey = modelObject.MembershipTypeFkNavigation.NameTranslationKey,
            StartDate = modelObject.StartDate,

        };
        return returnObject;
    }


    public static Application ToApplication(this ApplicationDTO modelObject)
    {
        Application returnObject = new()
        {
            Id = modelObject.Id,
            NameTranslationKey = modelObject.NameTranslationKey,
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
            PageFk = modelObject.PageFk,
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
            PositionIndex = modelObject.PositionIndex,
            ApplicationNameTranslationKey = modelObject.ApplicationFkNavigation.NameTranslationKey
        };
        return returnObject;
    }

    public static List<PageDTO> ToPageDTOList(this List<Page> modelObjectList)
    {
        List<PageDTO> returnList = [];
        modelObjectList.ForEach(modelObject =>
        {
            returnList.Add(modelObject.ToPageDTO()!);
        });
        return returnList;
    }

    public static PageDTO ToPageDTO(this Page modelObject)
    {
        PageDTO returnObject = new()
        {
            Id = modelObject.Id,
            ApplicationFk = modelObject.ApplicationFk,
            IsActive = modelObject.IsActive,
            IsApi = modelObject.IsApi,
            IsVisible = modelObject.IsVisible,
            NameTranslationKey = modelObject.NameTranslationKey,
            PositionIndex = modelObject.PositionIndex,
            ModuleFk = modelObject.ModuleFk,
            PageUrl = modelObject.PageUrl,
            ApplicationNameTranslationKey = modelObject.ApplicationFkNavigation.NameTranslationKey

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
            PageFk = modelObject.PageFk,
            DefaultPropertyValue = modelObject.DefaultPropertyValue,
            ApplicationFk = modelObject.ApplicationFk,
            PositionIndex = modelObject.PositionIndex,
            Id = modelObject.Id,
            NameTranslationKey = modelObject.NameTranslationKey,
            ObjectId = modelObject.ObjectId,
            ObjectName = modelObject.ObjectName,
            ObjectPropertyName = modelObject.ObjectPropertyName,
            ApplicationNameTranslationKey = modelObject.ApplicationFkNavigation.NameTranslationKey
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
            CreatedByUserFk = modelObject.CreatedByUserFk,
            CreatedDate = modelObject.CreatedDate,
            IsActive = modelObject.IsActive,
            RoleFk = modelObject.RoleFk,
            RoleNameTranslationKey = modelObject.RoleFkNavigation.NameTranslationKey,
            ApplicationFk = modelObject.ApplicationFk,
            ApplicationNameTranslationKey = modelObject.RoleFkNavigation.ApplicationFkNavigation.NameTranslationKey,
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
            AuditEntityFk = modelObject.AuditEntityFk,
            AuditInfoJson = modelObject.AuditInfoJson,
            CompanyFk = modelObject.CompanyFk,
            AuditEntityIndex = modelObject.AuditEntityIndex,
            AuditTypeFk = modelObject.AuditTypeFk,
            Browser = modelObject.Browser,
            CreationDate = modelObject.CreationDate,
            CultureFk = modelObject.CultureFk,
            DeviceType = modelObject.DeviceType,
            Engine = modelObject.Engine,
            HostName = modelObject.HostName,
            Id = modelObject.Id,
            IpAddress = modelObject.IpAddress,
            Platform = modelObject.Platform,
            UserFk = modelObject.UserFk,
            auditEntitySchemaName = modelObject.AuditEntityFkNavigation.SchemaName,
            AuditEntityName = modelObject.AuditEntityFkNavigation.EntityName,
            AuditTypeNameTranslationKey = modelObject.AuditTypeFkNavigation.NameTranslationKey,
            UserName = modelObject.UserFkNavigation.UserName,
            CompanyNameTranslationKey = modelObject.CompanyFkNavigation.NameTranslationKey
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
            EventDate = modelObject.EventDate,
            IpAddress = modelObject.IpAddress,
            Platform = modelObject.Platform,
            RequestEndpoint = modelObject.RequestEndpoint,
            TraceId = modelObject.TraceId,
            UserFk = modelObject.UserFk,
            UserName = modelObject.UserFkNavigation.UserName,
            CompanyNameTranslationKey = modelObject.UserFkNavigation.CompanyFkNavigation.NameTranslationKey,
            ActionTypeNameTranslationKey = modelObject.ActionTypeFkNavigation.NameTranslationKey

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
                CreationDate = modelObject!.CreationDate,
                IsActive = modelObject!.IsActive,
                LastUpdateByUserFk = modelObject!.LastUpdateByUserFk,
                LastUpdateDate = modelObject!.LastUpdateDate,
                Modules = new(),
                CompanyNameTranslationKey = modelObject.CompanyFkNavigation is null ? string.Empty : modelObject.CompanyFkNavigation.NameTranslationKey,
                ApplicationNameTranslationKey = modelObject.ApplicationFkNavigation.NameTranslationKey
            };

        modelObject!.RolePermissions.DistinctBy(x => x.ModuleFk).ToList().ForEach((permission) =>
        {
            ModuleDTO moduleDTO = new ModuleDTO();
            if (permission.ModuleFkNavigation is not null)
            {
                moduleDTO = permission.ModuleFkNavigation.ToModuleDTO();
                moduleDTO.Pages = new();
                permission.ModuleFkNavigation.Pages.ToList().ForEach((page) =>
                {
                    PageDTO pageDTO = new PageDTO();
                    pageDTO = page.ToPageDTO();
                    pageDTO.Components = new();
                    page.Components.ToList().ForEach((component) =>
                    {
                        pageDTO.Components.Add(component.ToComponentDTO());
                    });
                    moduleDTO.Pages.Add(pageDTO);
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

    public static NotificationDTO ToNotificationDTO(this Notification modelObject)
    {
        NotificationDTO returnObject;
        returnObject = new()
        {
            CultureFk = modelObject.CultureFk,
            Sender = modelObject.Sender,
            Subject = modelObject.Subject,
            Message = modelObject.Message,
            Receiver = modelObject.Receiver.Split(",").ToList(),
            Cc = modelObject.Cc!.Split(",").ToList(),
            Bcc = modelObject.Bcc!.Split(",").ToList(),
            NotificationTemplateGroupFk = modelObject.NotificationTypeFk!,
            CreationDate = modelObject.CreationDate,
            Id = modelObject.Id,
            IsHtml = modelObject.IsHtml,
            IsSent = modelObject.IsSent,
            NotificationTypeFk = modelObject.NotificationTypeFk,
            SentDate = modelObject.SentDate,

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
            CreationDate = modelObject.CreationDate,
            Id = modelObject.Id,
            IsHtml = modelObject.IsHtml,
            IsSent = modelObject.IsSent,
            SentDate = modelObject.SentDate,
        };
        return returnObject;
    }
}
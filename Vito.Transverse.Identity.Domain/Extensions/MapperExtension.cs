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
            CultureFk = modelObject.CultureFk,
            TranslationKey = modelObject.TranslationKey,
            TranslationValue = modelObject.TranslationValue
        };
        return returnObject;
    }

    public static List<ListItemDTO> ToListItemDTOList(this List<ListItem> modelObjectList)
    {
        List<ListItemDTO> returnList = [];
        modelObjectList.ForEach(modelObject =>
        {
            returnList.Add(modelObject.ToListItemDTO());
        });
        return returnList;
    }

    public static ListItemDTO ToListItemDTO(this ListItem modelObject)
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

    public static UserDTO ToUserDTO(this User modelObject, long? applicationId = null, string? applicationName = null, ActionTypeEnum actionStatus = ActionTypeEnum.ActionType_Undefined)
    {
        //var person = modelObject.PersonFkNavigation is not null ? modelObject.PersonFkNavigation.ToPersonDTO() : (new Person()).ToPersonDTO();
        var company = modelObject.CompanyFkNavigation is not null ? modelObject.CompanyFkNavigation.ToCompanyDTO() : (new Company()).ToCompanyDTO();
        var role = modelObject.RoleFkNavigation is not null ? modelObject.RoleFkNavigation.ToRoleDTO() : (new Role()).ToRoleDTO();
        UserDTO returnObject = new()
        {
            CompanyFk = modelObject.CompanyFk,
            Id = modelObject.Id,
            UserName = modelObject.UserName,
            Password = null,
            EmailValidated = modelObject.EmailValidated,
            IsLocked = modelObject.IsLocked,
            RequirePasswordChange = modelObject.RequirePasswordChange,
            RetryCount = modelObject.RetryCount,
            LastAccess = modelObject.LastAccess,
            IsActive = modelObject.IsActive,
            RoleFk = modelObject.RoleFk,
            Name = modelObject.Name,
            LastName = modelObject.LastName,
            Email = modelObject.Email,
            ApplicationId = applicationId,
            ApplicationName = applicationName,
            CompanyName = company.Name,
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
            RoleFk = modelObject.RoleFk,
            ActivationId = modelObject.ActivationId ?? Guid.NewGuid(),
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
            modelObject.Name,
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
            modelObject.IsActive
            );
        return returnObject;
    }


    public static Company ToCompany(this CompanyDTO modelObject)
    {
        Company returnObject = new()
        {
            Id = modelObject.Id,
            Name = modelObject.Name,
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
            Name = modelObject.Name,
            Avatar = modelObject.Avatar,
            IsActive = modelObject.IsActive,
            ApplicationSecret = modelObject.ApplicationSecret,
        };
        return returnObject;
    }

    public static Application ToApplication(this ApplicationDTO modelObject)
    {
        Application returnObject = new()
        {
            Id = modelObject.Id,
            Name = modelObject.Name,
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

    public static RoleDTO? ToRoleDTO(this Role? modelObject)
    {
        RoleDTO? returnObject;

        returnObject = modelObject is null ? null :
            new(
                modelObject!.Id,
                modelObject!.NameTranslationKey
        );
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
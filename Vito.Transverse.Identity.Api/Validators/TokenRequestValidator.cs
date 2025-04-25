using FluentValidation;
using Vito.Framework.Common.Constants;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Models.Security;
using Vito.Transverse.Identity.BAL.TransverseServices.Culture;
using Vito.Transverse.Identity.BAL.TransverseServices.Localization;

namespace Vito.Transverse.Identity.Api.Validators;

public class TokenRequestValidator : AbstractValidator<TokenRequestDTO>
{
    public TokenRequestValidator(ILocalizationService localizationService)//IStringLocalizer<TokenRequestDTO> localizer)
    {

        RuleFor(x => x.grant_type).NotEmpty().WithMessage(localizationService.GetLocalizedMessage("Validator_NotEmpty", "grant_type").TranslationValue);
        RuleFor(x => x.company_id).NotEmpty().WithMessage(localizationService.GetLocalizedMessage("Validator_NotEmpty", "company_id").TranslationValue);
        RuleFor(x => x).Custom((entity, vaidationContext) =>
        {
            if (IsValidGuid(entity.company_id))
            {
                vaidationContext.AddFailure(nameof(entity.company_id), localizationService.GetLocalizedMessage("Validator_NotNull", nameof(entity.company_id)).TranslationValue);
                //vaidationContext.AddFailure(nameof(entity.company_id), $"{nameof(entity.company_id)}{FrameworkConstants.ValidatorValidGuid}");
            }
        });

        RuleFor(x => x.company_secret).NotEmpty().WithMessage(localizationService.GetLocalizedMessage("Validator_NotEmpty", "company_secret").TranslationValue);
        RuleFor(x => x).Custom((entity, vaidationContext) =>
        {
            if (IsValidGuid(entity.company_secret))
            {
                vaidationContext.AddFailure(nameof(entity.company_secret), localizationService.GetLocalizedMessage("Validator_NotNull", nameof(entity.company_secret)).TranslationValue);
                //vaidationContext.AddFailure(nameof(entity.company_secret), $"{nameof(entity.company_secret)}{FrameworkConstants.ValidatorValidGuid}");
            }
        });

        RuleFor(x => x.application_id).NotEmpty().WithMessage(localizationService.GetLocalizedMessage("Validator_NotEmpty", "application_id").TranslationValue);
        RuleFor(x => x).Custom((entity, vaidationContext) =>
        {
            if (IsValidGuid(entity.application_id))
            {
                vaidationContext.AddFailure(nameof(entity.application_id), localizationService.GetLocalizedMessage("Validator_NotNull", nameof(entity.application_id)).TranslationValue);
                // vaidationContext.AddFailure(nameof(entity.application_id), $"{nameof(entity.application_id)}{FrameworkConstants.ValidatorValidGuid}");
            }
        });

        RuleFor(x => x).Custom((entity, vaidationContext) =>
        {
            if (!Enum.TryParse(entity.grant_type, true, out TokenGrantTypeEnum tokenGrantType))
            {
                vaidationContext.AddFailure(nameof(entity.grant_type), localizationService.GetLocalizedMessage("Validator_NotNull", nameof(entity.grant_type)).TranslationValue);
                //vaidationContext.AddFailure(nameof(entity.grant_type), $"{nameof(entity.grant_type)}{FrameworkConstants.ValidatorValidValue}");
            }
            else if (tokenGrantType == TokenGrantTypeEnum.RefreshToken)
            {
                vaidationContext.AddFailure(nameof(entity.grant_type), localizationService.GetLocalizedMessage("Validator_NotImplemented", nameof(entity.grant_type)).TranslationValue);
                //vaidationContext.AddFailure(nameof(entity.grant_type), $"{nameof(entity.grant_type)}{FrameworkConstants.ValidatorNotImplemented}");
            }
        });

        //AuthorizationCode - Company asumption <companyname>-api-user max role
        //RuleFor(x => x.application_secret).NotEmpty().When(x => VaidateGrantTypeAuthorizationCode(x));

        //ClientCredentials user info role assigned
        RuleFor(x => x.user_id).NotEmpty().When(x => VaidateGrantTypeClientCredentials(x))
            .WithMessage(localizationService.GetLocalizedMessage("Validator_NotEmpty", "user_id").TranslationValue); ;
        RuleFor(x => x.user_secret).NotEmpty().When(x => VaidateGrantTypeClientCredentials(x))
            .WithMessage(localizationService.GetLocalizedMessage("Validator_NotEmpty", "user_secret").TranslationValue); ;
    }

    private bool IsValidGuid(string? id)
    {
        bool isValid = (!Guid.TryParse(id, out Guid parsedGuid));
        return isValid;
    }

    private bool VaidateGrantTypeAuthorizationCode(TokenRequestDTO x)
    {
        var validate = x.grant_type.Equals(TokenGrantTypeEnum.AuthorizationCode.ToString(), StringComparison.InvariantCultureIgnoreCase);
        return validate;
    }

    private bool VaidateGrantTypeClientCredentials(TokenRequestDTO x)
    {
        var validate = x.grant_type.Equals(TokenGrantTypeEnum.ClientCredentials.ToString(), StringComparison.InvariantCultureIgnoreCase);
        return validate;
    }

}
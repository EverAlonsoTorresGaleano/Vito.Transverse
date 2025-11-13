using FluentValidation;
using Vito.Framework.Common.Enums;
using Vito.Framework.Common.Models.Security;

namespace Vito.Transverse.Identity.Presentation.Api.Validators;

public class TokenRequestValidator : AbstractValidator<TokenRequestDTO>
{
    public TokenRequestValidator()
    {
        RuleFor(x => x.grant_type).NotEmpty().WithMessage("Validator_NotEmpty_grant_type");
        RuleFor(x => x.company_id).NotEmpty().WithMessage("Validator_NotEmpty_company_id");
        RuleFor(x => x).Custom((entity, vaidationContext) =>
        {
            if (IsValidGuid(entity.company_id))
            {
                vaidationContext.AddFailure(nameof(entity.company_id), $"Validator_NotNull_{nameof(entity.company_id)}");
            }
        });

        //RuleFor(x => x.company_secret).NotEmpty().WithMessage("Validator_NotEmpty_company_secret");
        //RuleFor(x => x).Custom((entity, vaidationContext) =>
        //{
        //    if (IsValidGuid(entity.company_secret))
        //    {
        //        vaidationContext.AddFailure(nameof(entity.company_secret), $"Validator_NotNull_{nameof(entity.company_secret)}");
        //    }
        //});

        RuleFor(x => x.application_id).NotEmpty().WithMessage("Validator_NotEmpty_application_id");
        RuleFor(x => x).Custom((entity, vaidationContext) =>
        {
            if (IsValidGuid(entity.application_id))
            {
                vaidationContext.AddFailure(nameof(entity.application_id), $"Validator_NotNull{nameof(entity.application_id)}");
            }
        });

        RuleFor(x => x).Custom((entity, vaidationContext) =>
        {
            if (!Enum.TryParse(entity.grant_type, true, out TokenGrantTypeEnum tokenGrantType))
            {
                vaidationContext.AddFailure(nameof(entity.grant_type), $"Validator_NotNull{nameof(entity.grant_type)} ");
            }
            else if (tokenGrantType == TokenGrantTypeEnum.RefreshToken)
            {
                vaidationContext.AddFailure(nameof(entity.grant_type), $"Validator_NotImplemented{nameof(entity.grant_type)}");
            }
        });

        //AuthorizationCode - Company asumption <companyname>-api-user max role
        RuleFor(x => x.application_secret).NotEmpty().When(x => ValidateGrantTypeAuthorizationCode(x))
            .WithMessage("Validator_NotEmpty_application_secret");

        //ClientCredentials user info role assigned
        RuleFor(x => x.user_id).NotEmpty().When(x => ValidateGrantTypeClientCredentials(x))
            .WithMessage("Validator_NotEmpty_user_id");
        RuleFor(x => x.user_secret).NotEmpty().When(x => ValidateGrantTypeClientCredentials(x))
            .WithMessage("Validator_NotEmpty_user_secret");
    }

    private bool IsValidGuid(string? id)
    {
        bool isValid = (!Guid.TryParse(id, out Guid parsedGuid));
        return isValid;
    }

    private bool ValidateGrantTypeAuthorizationCode(TokenRequestDTO x)
    {
        var validate = x.grant_type.Equals(TokenGrantTypeEnum.AuthorizationCode.ToString(), StringComparison.InvariantCultureIgnoreCase);
        return validate;
    }

    private bool ValidateGrantTypeClientCredentials(TokenRequestDTO x)
    {
        var validate = x.grant_type.Equals(TokenGrantTypeEnum.ClientCredentials.ToString(), StringComparison.InvariantCultureIgnoreCase);
        return validate;
    }

}
using FluentValidation;

namespace Vito.Transverse.Identity.Api.Validators;

public class ActivationTokenValidator : AbstractValidator<string>
{

    public ActivationTokenValidator()
    {
        RuleFor(x => x).NotEmpty().WithMessage("Validator_NotEmpty_ActivationToken");
        RuleFor(x => x).Custom((entity, vaidationContext) =>
        {
            if (!IsActivationTokenValid(entity))
            {
                vaidationContext.AddFailure("ActivationToken", "Validator_NotValid_ActivationToken");
            }
        });


    }



    private bool IsActivationTokenValid(string activationToken)
    {
        bool isValid = false;
        try
        {
            var activationTokenList = activationToken.Split("@").ToList();
            if (activationTokenList.Count != 3)
            {
                return isValid;
            }
            Guid companyClientId = Guid.Parse(activationTokenList.First());
            long userId = long.Parse(activationTokenList[1]);
            Guid activationId = Guid.Parse(activationTokenList.Last());
            isValid = true;
        }
        catch
        {

        }
        return isValid;

    }
}

using FluentValidation;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace Vito.Transverse.Identity.Presentation.Api.Validators;

public class CultureTranslationValidator : AbstractValidator<CultureTranslationDTO>
{
    public CultureTranslationValidator() 
    {
        RuleFor(x => x.ApplicationFk)
            .GreaterThan(0)
            .WithMessage("ApplicationFk must be greater than 0");

        RuleFor(x => x.CultureFk)
            .NotEmpty()
            .WithMessage("CultureFk is required");

        RuleFor(x => x.TranslationKey)
            .NotEmpty()
            .WithMessage("TranslationKey is required");

        RuleFor(x => x.TranslationValue)
            .NotEmpty()
            .WithMessage("TranslationValue is required");
    }   
}


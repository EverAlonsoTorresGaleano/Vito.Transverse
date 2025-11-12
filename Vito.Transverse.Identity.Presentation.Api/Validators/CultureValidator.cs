using FluentValidation;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace Vito.Transverse.Identity.Presentation.Api.Validators;

public class CultureValidator : AbstractValidator<CultureDTO>
{
    public CultureValidator() { }
}


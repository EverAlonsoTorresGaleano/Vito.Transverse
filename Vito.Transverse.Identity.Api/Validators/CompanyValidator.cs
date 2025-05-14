using FluentValidation;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.Api.Validators;

public class CompanyValidator : AbstractValidator<ApplicationDTO>
{

    public CompanyValidator() { }   
}

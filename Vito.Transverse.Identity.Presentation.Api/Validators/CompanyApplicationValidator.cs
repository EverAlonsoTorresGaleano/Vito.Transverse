using FluentValidation;
using Vito.Transverse.Identity.Entities.DTO;
using Vito.Transverse.Identity.Entities.ModelsDTO;

namespace Vito.Transverse.Identity.Presentation.Api.Validators;

public class CompanyApplicationValidator : AbstractValidator<CompanyApplicationsDTO>
{

    public CompanyApplicationValidator() { }   
}

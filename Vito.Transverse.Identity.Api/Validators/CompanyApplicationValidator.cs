using FluentValidation;
using Vito.Transverse.Identity.Domain.DTO;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.Api.Validators;

public class CompanyApplicationValidator : AbstractValidator<CompanyApplicationsDTO>
{

    public CompanyApplicationValidator() { }   
}

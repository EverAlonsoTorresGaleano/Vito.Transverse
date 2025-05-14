using FluentValidation;
using Vito.Transverse.Identity.Domain.ModelsDTO;

namespace Vito.Transverse.Identity.Api.Validators;

public class UserValidator : AbstractValidator<UserDTO>
{
    public UserValidator() { }

}

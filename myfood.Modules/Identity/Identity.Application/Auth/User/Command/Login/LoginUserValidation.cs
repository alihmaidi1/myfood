using FluentValidation;

namespace Identity.Application.Auth.User.Command.Login;

public class LoginUserValidation: AbstractValidator<LoginUserRequest>
{

    public LoginUserValidation()
    {

        RuleFor(x => x.Password)
            .NotNull();

        RuleFor(x => x.Username)
            .NotNull();
    }
    
}
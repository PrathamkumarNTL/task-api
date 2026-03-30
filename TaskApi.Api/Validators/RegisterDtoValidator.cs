using FluentValidation;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Username).NotEmpty().MinimumLength(3);

        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
    }
}
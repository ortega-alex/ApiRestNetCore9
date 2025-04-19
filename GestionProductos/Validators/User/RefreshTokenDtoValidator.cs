using FluentValidation;
using GestionProductos.DTOs.User;

namespace GestionProductos.Validators.User
{
    public class RefreshTokenDtoValidator : AbstractValidator<RefreshTokenDto>
    {
        public RefreshTokenDtoValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is required.")
                .MinimumLength(3)
                .WithMessage("Username must be at least 3 characters lon. ");

            RuleFor(x => x.RefreshToken)
                .NotEmpty()
                .WithMessage("Refresh Token is required.");
        }
    }
}

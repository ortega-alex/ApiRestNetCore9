﻿using FluentValidation;
using GestionProductos.DTOs.User;

namespace GestionProductos.Validators.User
{
    public class LoginUserDtoValidator: AbstractValidator<LoginUserDto>
    {
        public LoginUserDtoValidator ()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage("Username is required.")
                .MinimumLength(3)
                .WithMessage("Username must be at least 3 characters lon. ");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .MinimumLength(6)
                .WithMessage("Password must be at least 6 characters long.");
        }
    }
}

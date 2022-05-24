using FluentValidation;
using SocialMedia.Core.DTOs;
using System;

namespace SocialMedia.Infraestructure.Validatios
{
    public class PostValidator: AbstractValidator<PostDTO>
    {
        public PostValidator()
        {
            RuleFor(pos => pos.Description)
                .NotNull()
                .WithMessage("la descripcion no puede ser nula");
            RuleFor(pos => pos.Description)
                .Length(10, 500)
            .WithMessage("la descripcion debe estar entre 10 y 500 caracteres");
            RuleFor(pos => pos.Date)
              .NotNull()
            .LessThan(DateTime.Now);
        }
    }
}
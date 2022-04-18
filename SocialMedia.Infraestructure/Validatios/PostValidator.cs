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
                .Length(10, 500);
            RuleFor(pos => pos.Date)
              .NotNull()
            .LessThan(DateTime.Now);
        }
    }
}
using FluentValidation;
using SocialMedia.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infraestructure.Validatios
{
    public class PostValidator: AbstractValidator<PostDTO>
    {
        public PostValidator()
        {
            RuleFor(pos => pos.Description)
                .NotNull()
                .Length(10, 15);
            RuleFor(pos => pos.Date)
              .NotNull()
            .LessThan(DateTime.Now);
        }
    }
}

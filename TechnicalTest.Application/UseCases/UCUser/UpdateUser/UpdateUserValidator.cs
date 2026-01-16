using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTest.Application.UseCases.UCUser.UpdateUser
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
            public UpdateUserValidator()
            {
                RuleFor(x => x.Request.Email)
                    .NotEmpty()
                    .EmailAddress()
                    .MaximumLength(50)
                    .When(x => x.Request.Email != null);

                RuleFor(x => x.Request.Name)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(50)
                    .When(x => x.Request.Name != null); ;

                RuleFor(x => x.Request.Password)
                    .NotEmpty()
                    .MinimumLength(8)
                    .MaximumLength(64)
                    .When(x => x.Request.Password != null); ;
            }
    }
}

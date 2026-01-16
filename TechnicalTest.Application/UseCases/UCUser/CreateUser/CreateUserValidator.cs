using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTest.Application.UseCases.UCUser.CreateUser
{
    public class CreateUserValidator : AbstractValidator<CreateUserRequest>
    {
            public CreateUserValidator()
            {
                RuleFor(x => x.Email)
                    .NotEmpty()
                    .EmailAddress()
                    .MaximumLength(50);

                RuleFor(x => x.Name)
                    .NotEmpty()
                    .MinimumLength(3)
                    .MaximumLength(50);

                RuleFor(x => x.Password)
                    .NotEmpty()
                    .MinimumLength(8)
                    .MaximumLength(64);
            }
    }
}

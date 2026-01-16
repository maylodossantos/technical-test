using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTest.Application.UseCases.UCUser.UpdateUser;

namespace TechnicalTest.Application.UseCases.UCUser.DeleteUser
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
            public UpdateUserValidator()
            {
                RuleFor(x => x.Id)
                    .NotEmpty()
                    .WithMessage("User id is required");
            }
    }
}

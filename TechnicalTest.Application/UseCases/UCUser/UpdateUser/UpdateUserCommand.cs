using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTest.Application.UseCases.UCUser.UpdateUser
{
    public sealed record UpdateUserCommand(Guid Id, UpdateUserRequest Request)
        : IRequest<UpdateUserResponse>;
}

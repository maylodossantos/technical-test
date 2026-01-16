using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTest.Application.UseCases.UCUser.CreateUser
{
    public sealed record CreateUserRequest(
        string Email,
        string Name,
        string Password
    ) : IRequest<CreateUserResponse>;
}

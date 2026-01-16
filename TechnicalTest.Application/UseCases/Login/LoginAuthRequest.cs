using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTest.Application.UseCases.Login
{
    public record LoginAuthRequest(string Username, string Password) : IRequest<LoginAuthResponse>;

}

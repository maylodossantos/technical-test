using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTest.Application.UseCases.Auth
{
    public record RefreshTokenRequest(string RefreshToken) : IRequest<RefreshTokenResponse>;

}

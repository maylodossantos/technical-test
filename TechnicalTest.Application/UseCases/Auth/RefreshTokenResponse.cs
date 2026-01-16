using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTest.Application.UseCases.Auth
{
    public record RefreshTokenResponse(string AcessToken, string RefreshToken);
}

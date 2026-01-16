using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechnicalTest.Application.UseCases.Auth;
using TechnicalTest.Application.UseCases.Login;

namespace TechnicalTest.API.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator mediator;

        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginAuthRequest request)
        {
            var login = new LoginAuthRequest(request.Username, request.Password);
            return Ok(await mediator.Send(login));
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshTokenRequest refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken.RefreshToken))
                return BadRequest("Refresh token is required.");
            var refreshTokenDeprecated = new RefreshTokenRequest(refreshToken.RefreshToken);
            var reponse = await mediator.Send(refreshTokenDeprecated);
            return Ok(reponse);
        }

    }
}

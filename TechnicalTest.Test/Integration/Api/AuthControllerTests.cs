using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTest.API.Controllers;
using Moq;
using MediatR;
using TechnicalTest.Application.UseCases.Login;
using Microsoft.AspNetCore.Mvc;
using TechnicalTest.Application.UseCases.Auth;

namespace TechnicalTest.Test.Integration.Api
{
    public class AuthControllerTests
    {
        private readonly Mock<IMediator> _mockMediator;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _mockMediator = new Mock<IMediator>();
            _controller = new AuthController(_mockMediator.Object);
        }

        [Fact]
        public async Task Login_ShouldReturnOk_WhenCredentialsAreValid()
        {
            // Arrange
            var loginRequest = new LoginAuthRequest("testuser", "123456");

            // Mock do MediatR: quando Send é chamado, retorna um objeto simulando a resposta
            _mockMediator
                .Setup(m => m.Send(loginRequest, It.IsAny<CancellationToken>()))
                .ReturnsAsync(new LoginAuthResponse(
                    "fake-access-token",  
                    "fake-refresh-token"  
                ));

            // Act
            var result = await _controller.Login(loginRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<LoginAuthResponse>(okResult.Value);

            Assert.Equal("fake-access-token", response.AccessToken);
            Assert.Equal("fake-refresh-token", response.RefreshToken);
        }

        [Fact]
        public async Task Refresh_ShouldReturnOk_WhenRefreshTokenIsValid()
        {
            // Arrange
            var refreshRequest = new RefreshTokenRequest("fake-refresh-token");

            _mockMediator
                .Setup(m => m.Send(It.Is<RefreshTokenRequest>(r => r.RefreshToken == "fake-refresh-token"), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new RefreshTokenResponse(
                    "new-access-token",   
                    "new-refresh-token"   
                ));

            // Act
            var result = await _controller.Refresh(refreshRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<RefreshTokenResponse>(okResult.Value);

            Assert.Equal("new-access-token", response.AcessToken);
        }
    }
}

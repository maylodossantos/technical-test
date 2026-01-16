using MediatR;
using TechnicalTest.Application.Services;
using TechnicalTest.Application.UseCases.Login;
using TechnicalTest.Domain.Entities;
using TechnicalTest.Domain.Interfaces;

public class LoginAuthHandler : IRequestHandler<LoginAuthRequest, LoginAuthResponse>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;

    public LoginAuthHandler(
            IUnitOfWork unitOfWork,
            IRefreshTokenRepository refreshTokenRepository,
            ITokenService tokenService,
            IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
        _refreshTokenRepository = refreshTokenRepository;
        _tokenService = tokenService;
        _userRepository = userRepository;
    }

    public async Task<LoginAuthResponse> Handle(LoginAuthRequest request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username, cancellationToken);

        if (user == null)
            throw new UnauthorizedAccessException("Invalid credentials");

        var accessToken = _tokenService.GenerateAccessToken(user);
        var refreshTokenValue = _tokenService.GenerateRefreshToken();

        var newRefreshTokenEntity = new RefreshToken(
            token: refreshTokenValue,
            userId: user.Id
        );

        await _refreshTokenRepository.AddAsync(newRefreshTokenEntity, cancellationToken);

        return new LoginAuthResponse(accessToken, refreshTokenValue);
    }

}

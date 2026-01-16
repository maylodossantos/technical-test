using AutoMapper;
using MediatR;
using TechnicalTest.Application.Services;
using TechnicalTest.Application.UseCases.Login;
using TechnicalTest.Domain.Interfaces;
using TechnicalTest.Domain.Entities;

namespace TechnicalTest.Application.UseCases.Auth.RefreshToken
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, RefreshTokenResponse>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public RefreshTokenHandler(
            IUnitOfWork unitOfWork,
            IRefreshTokenRepository refreshTokenRepository,
            ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _refreshTokenRepository = refreshTokenRepository;
            _tokenService = tokenService;
        }

        public async Task<RefreshTokenResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            var refreshTokenEntity = await _refreshTokenRepository.GetByTokenAsync(request.RefreshToken);

            if (refreshTokenEntity == null || refreshTokenEntity.IsExpired() || refreshTokenEntity.IsRevoked)
                throw new UnauthorizedAccessException();

            refreshTokenEntity.Revoke();
            await _refreshTokenRepository.UpdateAsync(refreshTokenEntity, cancellationToken);

            var accessToken = _tokenService.GenerateAccessToken(refreshTokenEntity.User);
            var newRefreshTokenValue = _tokenService.GenerateRefreshToken();

            var newRefreshTokenEntity = new Domain.Entities.RefreshToken(
                token: newRefreshTokenValue,
                userId: refreshTokenEntity.UserId 
            );

            await _refreshTokenRepository.AddAsync(newRefreshTokenEntity, cancellationToken);
            await _unitOfWork.Commit(cancellationToken);

            return new RefreshTokenResponse(accessToken, newRefreshTokenValue);
        }
    }
}

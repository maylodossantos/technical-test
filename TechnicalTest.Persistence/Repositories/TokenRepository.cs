using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TechnicalTest.Data.Context;
using TechnicalTest.Domain.Entities;
using TechnicalTest.Domain.Interfaces;

namespace TechnicalTest.Data.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly AppDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public RefreshTokenRepository(AppDbContext context)
        {
            _context = context;
            _unitOfWork = new UnitOfWork(_context);
        }

        public async Task<RefreshToken?> GetByTokenAsync(string token)
        {
            return await _context.RefreshTokens
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Token == token);
        }

        public async Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
        {
            await _context.RefreshTokens.AddAsync(refreshToken);
            await _unitOfWork.Commit(cancellationToken);
        }
        public async Task UpdateAsync(RefreshToken refreshToken, CancellationToken cancellationToken)
        {
            _context.RefreshTokens.Update(refreshToken);
            await _unitOfWork.Commit(cancellationToken);
        }

    }
}

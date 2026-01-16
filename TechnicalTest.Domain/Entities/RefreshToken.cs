using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTest.Domain.Entities
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; private set; }
        public bool IsRevoked { get; private set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public RefreshToken(string token, Guid userId)
        {
            Id = Guid.NewGuid();
            Token = token;
            ExpiresAt = DateTime.UtcNow.AddDays(7);
            UserId = userId;
            IsRevoked = false;
        }

        public bool IsExpired()
           => DateTime.UtcNow >= ExpiresAt;

        public void Revoke()
            => IsRevoked = true;
    }
}

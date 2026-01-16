using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TechnicalTest.Domain.Entities
{
    public sealed class User : BaseEntity
    {

        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; private set; }

        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
            RefreshTokens = new List<RefreshToken>();
        }


    }
}

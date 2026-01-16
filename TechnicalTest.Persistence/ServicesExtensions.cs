using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTest.Application.Services;
using TechnicalTest.Data.Context;
using TechnicalTest.Data.Repositories;
using TechnicalTest.Data.Services;
using TechnicalTest.Domain.Interfaces;

namespace TechnicalTest.Data
{
    public static class ServicesExtensions
    {
        public static void ConfigureDataApp(this IServiceCollection services,
                IConfiguration configuration)
        {
           services.AddDbContext<AppDbContext>(opt => 
                    opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"), 
                    b => b.MigrationsAssembly("TechnicalTest.Data")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<ITokenService, TokenService>();
        }

    }
}

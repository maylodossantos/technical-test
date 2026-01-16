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

            var connectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION")
                                   ?? configuration.GetConnectionString("DefaultConnection")
                                   ?? "Host=localhost;Port=5432;Database=technicaltest;Username=postgres;Password=postgres";

            services.AddDbContext<AppDbContext>(opt =>
                opt.UseNpgsql(connectionString, b => b.MigrationsAssembly("TechnicalTest.Data"))
            );

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<ITokenService, TokenService>();
        }

    }
}

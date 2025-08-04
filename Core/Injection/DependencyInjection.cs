using Data.Context;
using Google;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Services;
using Data.Repository;
using Mapster;

namespace Core.Injection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddContext(IServiceCollection services)
        {
            services.AddDbContext<MasaiContxet>(options =>
                options.UseSqlServer("Server=.;Database=Masai_Web;Trusted_Connection=True;TrustServerCertificate=True")
            );
            return services;
        }

        public static IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserSerivce>();

            return services;
        }

        public static IServiceCollection AddMapper(IServiceCollection services)
        {
            services.AddMapster();
            return services;
        }
    }
}
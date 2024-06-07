using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MotorRental.Infrastructure.Data
{
    public static class DependencyInjection
    {
        public static IServiceCollection ImplementPersistence
            (this IServiceCollection services,
             IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(
                options1 => options1.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)),
                ServiceLifetime.Transient
            );

            return services;
        }
    }
}

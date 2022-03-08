using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MegaFortnite.DataAccess
{
    public static class Bootstrap
    {
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MegaFortniteDbContext>((sp, options)
                => options.UseNpgsql(configuration.GetConnectionString("default")));

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}

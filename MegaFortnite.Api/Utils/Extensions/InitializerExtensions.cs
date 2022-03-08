using MegaFortnite.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MegaFortnite.Api.Utils.Extensions
{
    public static class InitializerExtensions
    {
        public static IApplicationBuilder Migrate(this IApplicationBuilder appBuilder)
        {
            using var scope = appBuilder.ApplicationServices.CreateScope();
            using var context = scope.ServiceProvider.GetRequiredService<MegaFortniteDbContext>();
            context.Database.Migrate();
            return appBuilder;
        }

    }
}

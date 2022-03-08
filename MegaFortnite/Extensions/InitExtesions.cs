using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaFortnite.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MegaFortnite.Extensions
{
    public static class InitExtensions
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

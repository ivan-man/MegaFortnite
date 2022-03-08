using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MegaFortnite.Engine
{
    public static class Bootstrap
    {
        public static IServiceCollection AddEngine(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services;
        }
    }
}
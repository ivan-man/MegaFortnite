using FluentValidation;
using Mapster;
using MediatR;
using MegaFortnite.Business.Behaviors;
using MegaFortnite.Business.InternalServices;
using MegaFortnite.DataAccess;
using MegaFortnite.Engine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MegaFortnite.Business
{
    public static class Bootstrap
    {
        public static IServiceCollection AddBusinessLayer(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            TypeAdapterConfig.GlobalSettings.Scan(typeof(Bootstrap).Assembly, typeof(Bootstrap).Assembly);

            return services
                .AddSingleton<ILobbyManager, LobbyManager>()
                .AddDataAccess(configuration)
                .AddValidatorsFromAssembly(typeof(Bootstrap).Assembly)
                .AddMediatR(typeof(Bootstrap).Assembly)
                .AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>))
                .AddEngine(configuration);
        }              

    }
}

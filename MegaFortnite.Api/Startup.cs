using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.AccessTokenValidation;
using MegaFortnite.Api.Hubs;
using MegaFortnite.Api.Utils.Extensions;
using MegaFortnite.Business;
using MegaFortnite.Common.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace MegaFortnite.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MegaFortnite.Api", Version = "v1" });
            });

            // services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
            //     .AddIdentityServerAuthentication(options =>
            //     {
            //         // base-address of your identityserver
            //         options.Authority = "https://localhost:5001";
            //         options.LegacyAudienceValidation = true;
            //
            //         // name of the API resource
            //         options.ApiName = "test.api";
            //         options.ApiSecret = "secret";
            //         options.RequireHttpsMetadata = false;
            //     });

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://localhost:5001";
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        // IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                    options.Events= new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["token"];

                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken)
                                && (path.StartsWithSegments("/lobbyhub")
                                    || path.StartsWithSegments("/test")))
                            {
                                // Read the token out of the query string
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });


            services.AddScoped<INotifyService, NotifyService>();
            services.AddSignalR(options => options.EnableDetailedErrors = true);
            services.AddBusinessLayer(Configuration);
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDefaultFiles();
            app.Migrate();
            app.UseCors();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MegaFortnite.Api v1"));
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.Use(async (context, next) => await MiddleWare.AuthMiddleware.AuthQueryStringToHeader(context, next));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<LobbyHub>("/lobbyHub");
                endpoints.MapControllers();
            });
        }
    }
}

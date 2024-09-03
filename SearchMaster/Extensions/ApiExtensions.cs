using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SearchMaster.Core.Enum;
using SearchMaster.Infrastructure;
using System.Text;

namespace SearchMaster.Extensions
{
    public static class ApiExtensions
    {
        public const string ClientModeratorPolicy = "ClientAndModerator";
        public const string WorkerModeratorPolicy = "WorkerAndModerator";
        public const string ClientWorkerModeratorPolicy = "ClientWorkerModerator";

        public static IServiceCollection AddApiAuthentication(
            this IServiceCollection service,
            IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

            service.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        ValidateActor = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["asp"];

                            return Task.CompletedTask;
                        }
                    };
                });

            service.AddAuthorization(options =>
            {
                options.AddPolicy(ClientModeratorPolicy, policy =>
                {
                    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireRole([nameof(Roles.Client), nameof(Roles.Admin), nameof(Roles.Moderator)]);
                });

                options.AddPolicy(WorkerModeratorPolicy, policy =>
                {
                    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireRole([nameof(Roles.Worker), nameof(Roles.Admin), nameof(Roles.Moderator)]);
                });

                options.AddPolicy(ClientWorkerModeratorPolicy, policy =>
                {
                    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireRole([nameof(Roles.Client), nameof(Roles.Worker), nameof(Roles.Admin), nameof(Roles.Moderator)]);
                });
            });

            return service;
        }
    }
}

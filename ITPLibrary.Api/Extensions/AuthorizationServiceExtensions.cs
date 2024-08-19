using System.Text;
using AutoMapper;
using ITPLibrary.Core.Services;
using ITPLibrary.Core.Services.IServices;
using ITPLibrary.Data.Repositories.IRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ITPLibrary.Api.Extensions
{
    public static class AuthorizationServiceExtensions
    {
        public static IServiceCollection AddAuthorizationServices(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            var jwtSecret = config["Jwt:Secret"];

            services.AddScoped<IUserService, UserService>(
                provider =>
                    new UserService(
                        provider.GetRequiredService<IMapper>(),
                        provider.GetRequiredService<IUserRepository>(),
                        jwtSecret!,
                        provider.GetRequiredService<IEmailService>()
                    )
            );

            services.AddScoped<IEmailService, EmailService>(
                provider =>
                    new EmailService(
                        config["EmailSettings:SmtpServer"],
                        int.Parse(config["EmailSettings:SmtpPort"]),
                        config["EmailSettings:FromEmail"],
                        config["EmailSettings:FromPassword"]
                    )
            );

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.ASCII.GetBytes(jwtSecret!)
                        ),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;
        }
    }
}

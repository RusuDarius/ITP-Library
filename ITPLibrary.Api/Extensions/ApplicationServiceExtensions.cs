using Data;
using ITPLibrary.Core.Services;
using ITPLibrary.Core.Services.IServices;
using ITPLibrary.Data.Repositories;
using ITPLibrary.Data.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace ITPLibrary.Api.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration config
        )
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(config.GetConnectionString("DefaultConnection"))
            );

            services.AddCors(opt =>
            {
                opt.AddPolicy(
                    "CorsPolicy",
                    policy =>
                    {
                        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
                    }
                );
            });

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}

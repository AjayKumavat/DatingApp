using API.Data;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    //Static means we don't have to create an instance of this class in order to use it
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            //AddScoped : It will scope the service till the life time of the HTTP request
            services.AddScoped<ITokenService, TokenService>();
            //User Repository
            services.AddScoped<IUserRepository, UserRepository>();
            //Mapping AppUser and MemberDto using AutoMapper
            services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
            //Ordering here is not important
            //Connection String
            // Ordering is not important here
            //Injecting this so other classes can use the datacontext
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("con"));
            });

            return services;
        }
    }
}
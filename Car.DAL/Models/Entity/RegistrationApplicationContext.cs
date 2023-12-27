using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarWebService.DAL.Models.Entity
{
    public static class RegistrationApplicationContext
    {
        public static IServiceCollection AddApplicationContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
            return services;
        }
    }
}
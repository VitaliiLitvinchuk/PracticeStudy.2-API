using Core.Entities.Identity;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace PracticeStudy._2.Inits
{
    public static class InitDbContext
    {
        public static IServiceCollection UseDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PracticeDBContext>(options =>
                options.UseLazyLoadingProxies().UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            }).AddEntityFrameworkStores<PracticeDBContext>().AddDefaultTokenProviders();

            return services;
        }
    }
}

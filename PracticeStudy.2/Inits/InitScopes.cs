using Core.Entities.Machine;
using Core.Interfaces.Services;
using Core.Interfaces.Services.Machine;
using Core.Services;
using Core.ViewModels.Machine;
using Middleware.Managers;
using Middleware.Managers.Interfaces;
using PracticeStudy._2.Services;
using Serilog;

namespace PracticeStudy._2.Inits
{
    public static class InitScopes
    {
        public static IServiceCollection UseScopes(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ICarService, CarService>();

            services.AddScoped<IManager<PropertyViewModel, Property, string>, SmallManager<PropertyViewModel, Property, string>>();
            services.AddScoped<IManager<CarBrandViewModel, CarBrand, string>, SmallManager<CarBrandViewModel, CarBrand, string>>();
            services.AddScoped<IManager<CarYearViewModel, CarYear, int>, SmallManager<CarYearViewModel, CarYear, int>>();

            services.AddScoped<IManager<CharacteristicViewModel, Characteristic, Guid>, CharacteristicManager>();
            services.AddScoped<IManager<CarPhotoViewModel, CarPhoto, string>, CarPhotoManager>();
            services.AddScoped<IManager<CarViewModel, Car, Guid>, CarManager>();

            return services;
        }
    }
}

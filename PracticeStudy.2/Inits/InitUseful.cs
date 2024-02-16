using Core.Validators;
using Core.Validators.Machine;
using FluentValidation.AspNetCore;
using Serilog;

namespace PracticeStudy._2.Inits
{
    public static class InitUseful
    {
        public static IServiceCollection UseUsefulNuGets(this IServiceCollection services, IConfiguration configuration)
        {
            // Add AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Add FluentValidation
            services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<LoginValidator>());
            services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CarValidator>());

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.File(path: Path.Combine(Directory.GetCurrentDirectory(), "logs", $"log-{DateTime.Now:yyyy-MM-dd}.txt"))
                .CreateLogger();
            
            services.AddLogging(b => b.AddSerilog(dispose: true));

            return services;
        }
    }
}

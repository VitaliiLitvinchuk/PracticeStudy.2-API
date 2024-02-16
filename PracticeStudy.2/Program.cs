using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Middleware;
using PracticeStudy._2.Inits;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Middleware.Handler;

namespace PracticeStudy._2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllersWithViews();

            builder.Services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "client-app";
            });

            builder.Services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Include;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });

            builder.Services.AddRouting(options => options.LowercaseUrls = true);

            builder.Services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin()
                                                             .AllowAnyHeader()
                                                             .AllowAnyMethod());
            });

            builder.Services
                .UseDatabaseContext(builder.Configuration)
                .UseUsefulNuGets(builder.Configuration)
                .UseAuthJWT(builder.Configuration)
                .UseScopes()
                .AddEndpointsApiExplorer()
                .UseSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app
                .UseStaticFiles()
                .UseSpaStaticFiles();

            app.UseRouting();

            app.UseCors("AllowOrigin");

            app.UseHttpsRedirection();

            app
                .UseSwagger()
                .UseSwaggerUI();

            app.UseAppStaticFiles();

            app
                .UseAuthentication()
                .UseAuthorization();
            
            app.UseMiddleware<CustomExceptionHandler>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "client-app";

                if (app.Environment.IsDevelopment())
                    spa.UseReactDevelopmentServer(npmScript: "start");
            });

            app.SeedData();

            app.Run();
        }
    }
}

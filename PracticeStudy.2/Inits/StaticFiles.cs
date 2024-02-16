using Microsoft.Extensions.FileProviders;

namespace PracticeStudy._2.Inits
{
    public static class StaticFiles
    {
        public static IApplicationBuilder UseAppStaticFiles(this IApplicationBuilder app)
        {
            var dir = Path.Combine(Directory.GetCurrentDirectory(), "uploads");

            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(dir),
                RequestPath = "/images"
            });

            return app;
        }
    }
}

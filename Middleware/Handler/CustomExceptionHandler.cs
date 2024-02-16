using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Middleware.Handler
{
    public class CustomExceptionHandler(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                BadRequestResponse problem = new()
                {
                    Errors = new { message = e.Message },
                };

                context.Response.ContentType = "application/json";

                if (e is ArgumentException || e is ArgumentNullException)
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                else if (e is DbUpdateException)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    problem.Errors = new { message = "This data exist" };
                }
                else
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
            }
        }
    }
}

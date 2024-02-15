
using RepoAPI.Exceptions;
using System.Data;

namespace RepoAPI.Middlewares
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            try
            {
                await next.Invoke(context);
            }catch(ReadDataException dataException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync($"Status code: { 404 }\n");
                await context.Response.WriteAsync($"Message: { dataException.Message }");
            }catch(Exception e)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(e.Message);
            }
        }
    }
}

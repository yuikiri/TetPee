namespace TetPee.Api.Middlewares;

public class GlobalExceptionHandlerMiddlewares : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("[GlobalExceptionHandlerMiddlewares]" + ex);
        }
    }
}
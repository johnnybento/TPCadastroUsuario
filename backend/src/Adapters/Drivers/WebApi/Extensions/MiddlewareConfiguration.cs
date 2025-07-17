using TPCadastroUsuario.Adapters.Drivers.WebApi.Middleware;

namespace TPCadastroUsuario.Adapters.Drivers.WebApi.Extensions
{
    public static class MiddlewareConfiguration
    {
        public static WebApplication UseExceptionHandling(this WebApplication app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app;
        }
    }
}

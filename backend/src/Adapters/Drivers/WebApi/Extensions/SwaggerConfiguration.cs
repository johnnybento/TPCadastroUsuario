using Microsoft.OpenApi.Models;

namespace TPCadastroUsuario.Adapters.Drivers.WebApi.Extensions;

public static class SwaggerConfiguration
{
    public static WebApplication UsarSwaggerSomenteEmDev(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        return app;
    }
    public static void AddDocumentacaoSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Teste Prático Cadastro de Usuário API",
                Version = "v1",
                Description = "API para cadastro de usuários com validação de e-mail e senha, seguindo boas práticas de desenvolvimento.",

            });
        });
    }


}

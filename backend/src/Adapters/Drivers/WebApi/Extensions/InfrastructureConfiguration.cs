using Microsoft.EntityFrameworkCore;
using TPCadastroUsuario.Adapters.Driven.Infrastructure.Data;
using TPCadastroUsuario.Adapters.Driven.Infrastructure.Repositorios.Usuarios;
using TPCadastroUsuario.Adapters.Driven.Infrastructure.Services.Hashing;
using TPCadastroUsuario.Adapters.Driven.Infrastructure.Services.JWT;
using TPCadastroUsuario.Application.Common.Ports;
using TPCadastroUsuario.Core.Repositories;

namespace TPCadastroUsuario.Adapters.Drivers.WebApi.Extensions;

public static class InfrastructureConfiguration
{

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        IConfiguration configurationAppsettings = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
        services.AddDbContext<ApplicationDBContext>(options =>
            options.UseSqlServer(configurationAppsettings.GetConnectionString("ConnectionString")));

        services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

        services.AddScoped<ISenhaHasher, SenhaHasher>();
        services.AddSingleton<IJwtService, JwtService>();
        
        return services;
    }

}

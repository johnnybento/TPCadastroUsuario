using Microsoft.EntityFrameworkCore;
using TPCadastroUsuario.Adapters.Driven.Infrastructure.Data;

namespace TPCadastroUsuario.Adapters.Drivers.WebApi.Extensions;

public static class DatabaseConfiguration
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TPCadastroUsuario.Core.Entities;

namespace TPCadastroUsuario.Adapters.Driven.Infrastructure.Data;

public class ApplicationDBContext : DbContext
{
    public DbSet<Usuario> Usuarios { get; set; } = null!;

    private readonly string _connectionString;




    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
    : base(options)
    {
    }
    //public ApplicationDBContext()
    //{
    //    IConfiguration configuration = new ConfigurationBuilder().SetBasePath(AppDomain.CurrentDomain.BaseDirectory).AddJsonFile("appsettings.json").Build();
    //    _connectionString = configuration.GetConnectionString("ConnectionString");
    //}



    public ApplicationDBContext(string connectionString)
    {
        _connectionString = connectionString;
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDBContext).Assembly);
    }

}
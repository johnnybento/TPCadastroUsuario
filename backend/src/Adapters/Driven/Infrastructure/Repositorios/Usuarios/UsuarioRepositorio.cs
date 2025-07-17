using Microsoft.EntityFrameworkCore;
using TPCadastroUsuario.Adapters.Driven.Infrastructure.Data;
using TPCadastroUsuario.Adapters.Driven.Infrastructure.Repositorios.Common;
using TPCadastroUsuario.Core.Entities;
using TPCadastroUsuario.Core.Repositories;

namespace TPCadastroUsuario.Adapters.Driven.Infrastructure.Repositorios.Usuarios;

public class UsuarioRepositorio : EfRepository<Usuario, Guid>, IUsuarioRepositorio
{
    public UsuarioRepositorio(ApplicationDBContext ctx)
    : base(ctx) { }
    public async Task<Usuario?> BuscaPorEmailAsync(string email)
    {
        var usuarios = await _dbSet.AsNoTracking().ToListAsync();
        return usuarios.FirstOrDefault(u => u.Email.Valor == email);

        //return await _dbSet.FirstOrDefaultAsync(u => u.Email.Valor == email);
    }
    public async Task<bool> VeriricaSeExisteEmailAsync(string email)
    {
        var usuarios = await _dbSet.AsNoTracking().ToListAsync();
        return usuarios.Any(u => u.Email.Valor == email);
        //return await _dbSet.AnyAsync(u => u.Email.Valor == email);
    }
}


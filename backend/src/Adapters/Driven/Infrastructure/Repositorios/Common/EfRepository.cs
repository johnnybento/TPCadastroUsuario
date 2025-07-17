using Microsoft.EntityFrameworkCore;
using TPCadastroUsuario.Adapters.Driven.Infrastructure.Data;
using TPCadastroUsuario.Core.Entities;
using TPCadastroUsuario.Core.Repositories;

namespace TPCadastroUsuario.Adapters.Driven.Infrastructure.Repositorios.Common;

public class EfRepository<EntidadeBase, TId> : IRepositorio<EntidadeBase, TId> where EntidadeBase : EntidadeBase<TId>
{
    protected ApplicationDBContext _appDbContext;
    protected DbSet<EntidadeBase> _dbSet;
    public EfRepository(ApplicationDBContext context)
    {
        _appDbContext = context;
        _dbSet = _appDbContext.Set<EntidadeBase>();
    }
    public async Task AddAsync(EntidadeBase entidade)
    {
        await _dbSet.AddAsync(entidade);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task AtualizarAsync(EntidadeBase entidade)
    {
        _dbSet.Update(entidade);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task<EntidadeBase?> BuscaPorIdAsync(TId id)
        => await _dbSet.FindAsync(id);

    public async Task<IReadOnlyList<EntidadeBase>> ListarAsync()
        => await _dbSet.ToListAsync();

    public async Task RemoverAsync(TId id)
    {
        var entidade = await _dbSet.FindAsync(id);
        if (entidade != null)
        {
            _dbSet.Remove(entidade);
            await _appDbContext.SaveChangesAsync();
        }
    }
}

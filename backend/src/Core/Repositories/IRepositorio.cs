using TPCadastroUsuario.Core.Entities;

namespace TPCadastroUsuario.Core.Repositories;
/// <summary>
/// Operações genéricas de repositório para entidades com intuito de progressão do projeto
/// </summary>
public interface IRepositorio<EntidadeBase, TId> where
       EntidadeBase : EntidadeBase<TId>
{
    /// <summary>
    /// Busca específica de uma entidade pelo ID (select by id)
    /// </summary>
    Task<EntidadeBase?> BuscaPorIdAsync(TId id);
    /// <summary>
    /// Listar todas as entidades do repositório(select) 
    /// </summary>
    Task<IReadOnlyList<EntidadeBase>> ListarAsync();
    /// <summary>
    /// Adicionar a entidade no repositório, ou seja, inserir na tabela 
    /// </summary>
    Task AddAsync(EntidadeBase entidade);
    /// <summary>
    /// Atualizar a entidade no repositório
    /// </summary>
    Task AtualizarAsync(EntidadeBase entidade);
    /// <summary>
    /// Operação de remover a entidade do repositório ou seja deletar do banco
    /// </summary>
    Task RemoverAsync(TId id);
}
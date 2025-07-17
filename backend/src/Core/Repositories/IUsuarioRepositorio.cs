using TPCadastroUsuario.Core.Entities;

namespace TPCadastroUsuario.Core.Repositories;
/// <summary>
/// Operações específicas de consulta de Usuario\Email
/// </summary>
public interface IUsuarioRepositorio : IRepositorio<Usuario, Guid>
{
    /// <summary>Encontra um usuário pelo e-mail (ou retorna null).</summary>
    Task<Usuario> BuscaPorEmailAsync(string email);

    /// <summary>Verifica se já existe um usuário com este e-mail.</summary>
    Task<bool> VeriricaSeExisteEmailAsync(string email);

}

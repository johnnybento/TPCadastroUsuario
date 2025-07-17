using TPCadastroUsuario.Core.Entities;

namespace TPCadastroUsuario.Application.Common.Ports;
public interface IJwtService
{
    /// <summary>
    /// Gera um token JWT para o usuário informado.
    /// </summary>
    string GenerateToken(Usuario usuario);
}
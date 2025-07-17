using MediatR;

namespace TPCadastroUsuario.Application.Usuarios.Commands.CriarUsuario;
public class CriarUsuarioCommand : IRequest<CriarUsuarioDto>
{
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Senha { get; set; } = null!;
}
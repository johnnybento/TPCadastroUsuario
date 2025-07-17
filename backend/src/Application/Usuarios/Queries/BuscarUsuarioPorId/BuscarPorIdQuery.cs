using MediatR;

namespace TPCadastroUsuario.Application.Usuarios.Queries.BuscarUsuarioPorId;
public class BuscarPorIdQuery : IRequest<BucarPorIdUsuarioDto?>
{
    public Guid Id { get; set; }
}

using MediatR;
using TPCadastroUsuario.Core.Repositories;

namespace TPCadastroUsuario.Application.Usuarios.Queries.ListarUsuarios;

public class ListarUsuariosHandler : IRequestHandler<ListarUsuariosQuery, IEnumerable<ListarUsuarioDto>>
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    public ListarUsuariosHandler(IUsuarioRepositorio repo) => _usuarioRepositorio = repo;

    public async Task<IEnumerable<ListarUsuarioDto>> Handle(ListarUsuariosQuery request, CancellationToken cancellationToken)
    {
        var usuarios = await _usuarioRepositorio.ListarAsync();
        return usuarios.Select(u => new ListarUsuarioDto
        {
            Id = u.Id,
            Nome = u.Nome,
            Email = u.Email.Valor
        });
    }
}


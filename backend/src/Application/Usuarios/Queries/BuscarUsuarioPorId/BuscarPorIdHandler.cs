using MediatR;
using TPCadastroUsuario.Core.Repositories;

namespace TPCadastroUsuario.Application.Usuarios.Queries.BuscarUsuarioPorId;
public class BuscarPorIdHandler : IRequestHandler<BuscarPorIdQuery, BucarPorIdUsuarioDto?>
{

    private readonly IUsuarioRepositorio _usuarioRepositorio;

    public BuscarPorIdHandler(IUsuarioRepositorio repo) => _usuarioRepositorio = repo;

    public async Task<BucarPorIdUsuarioDto?> Handle(BuscarPorIdQuery request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepositorio.BuscaPorIdAsync(request.Id);
        if (usuario is null) return null;

        return new BucarPorIdUsuarioDto
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email.Valor
        };
    }
}


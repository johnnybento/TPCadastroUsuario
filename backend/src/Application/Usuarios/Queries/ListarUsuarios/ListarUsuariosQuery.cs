using MediatR;

namespace TPCadastroUsuario.Application.Usuarios.Queries.ListarUsuarios;
public class ListarUsuariosQuery : IRequest<IEnumerable<ListarUsuarioDto>> { }
    


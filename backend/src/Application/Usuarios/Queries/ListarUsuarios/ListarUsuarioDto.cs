namespace TPCadastroUsuario.Application.Usuarios.Queries.ListarUsuarios;

public  class ListarUsuarioDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
}

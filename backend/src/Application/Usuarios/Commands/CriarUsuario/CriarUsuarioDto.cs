namespace TPCadastroUsuario.Application.Usuarios.Commands.CriarUsuario;
public class CriarUsuarioDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
}
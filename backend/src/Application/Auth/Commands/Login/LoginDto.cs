namespace TPCadastroUsuario.Application.Auth.Commands.Login;

public record LoginDto(
    string Token,
    Guid UsuarioId,
    string Email
);

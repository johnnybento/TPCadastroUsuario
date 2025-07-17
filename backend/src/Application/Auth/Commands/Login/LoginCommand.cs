using MediatR;

namespace TPCadastroUsuario.Application.Auth.Commands.Login;
public record LoginCommand(
 string Email,
 string Senha
) : IRequest<LoginDto>;

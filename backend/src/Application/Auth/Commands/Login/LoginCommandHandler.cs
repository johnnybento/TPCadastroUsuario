using MediatR;
using TPCadastroUsuario.Application.Common.Ports;
using TPCadastroUsuario.Core.Exceptions;
using TPCadastroUsuario.Core.Repositories;

namespace TPCadastroUsuario.Application.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginDto>
{

    private readonly IUsuarioRepositorio _usuarioRepository;
    private readonly ISenhaHasher _senhaHasher;
    private readonly IJwtService _jwtService;


    public LoginCommandHandler(IUsuarioRepositorio usuarioRepository, ISenhaHasher senhaHasher, IJwtService jwtService)
    {
        _usuarioRepository = usuarioRepository;
        _senhaHasher = senhaHasher;
        _jwtService = jwtService;
    }

    public async Task<LoginDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var usuario = await _usuarioRepository.BuscaPorEmailAsync(request.Email);
        if (usuario == null)
            throw new DomainException("Credenciais inválidas.");

        if (!_senhaHasher.Verify(request.Senha, usuario.SenhaHash))
            throw new DomainException("Credenciais inválidas.");

        var token = _jwtService.GenerateToken(usuario);

        return new LoginDto(
            Token: token,
            UsuarioId: usuario.Id,
            Email: usuario.Email.Valor
        );
    }
}

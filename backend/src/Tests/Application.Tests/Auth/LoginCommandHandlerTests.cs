using FluentAssertions;
using Moq;
using System.Reflection.Metadata;
using TPCadastroUsuario.Application.Auth.Commands.Login;
using TPCadastroUsuario.Application.Common.Ports;
using TPCadastroUsuario.Application.Tests.Auth.TestData;
using TPCadastroUsuario.Core.Entities;
using TPCadastroUsuario.Core.Exceptions;
using TPCadastroUsuario.Core.Repositories;

namespace TPCadastroUsuario.Application.Tests.Auth;

public class LoginCommandHandlerTests
{
    private readonly Mock<IUsuarioRepositorio> _usuarioRepositorio;
    private readonly Mock<ISenhaHasher> _senhaHasher;
    private readonly Mock<IJwtService> _jwtService;
    private readonly LoginCommandHandler _loginHandler;
    private readonly LoginCommandTestData _loginTestDataFixture;


    public LoginCommandHandlerTests()
    {
        _usuarioRepositorio = new Mock<IUsuarioRepositorio>();
        _senhaHasher = new Mock<ISenhaHasher>();
        _jwtService = new Mock<IJwtService>();
        _loginHandler = new LoginCommandHandler(
            _usuarioRepositorio.Object,
            _senhaHasher.Object,
            _jwtService.Object);
        _loginTestDataFixture = new LoginCommandTestData();
    }
    [Fact]
    public async Task Handler_QuandoAsCredenciaisSaoValidas_DeveRetornarDtoComTokenGerado()
    {
        // Arrange
        var cmd = _loginTestDataFixture.LoginRequest;
        var usuario = _loginTestDataFixture.GetUsuario();
        var token = Guid.NewGuid().ToString();

        _usuarioRepositorio
            .Setup(r => r.BuscaPorEmailAsync(cmd.Email))
            .ReturnsAsync(usuario);

        _senhaHasher
            .Setup(h => h.Verify(cmd.Senha, usuario.SenhaHash))
            .Returns(true);

        _jwtService
            .Setup(j => j.GenerateToken(usuario))
            .Returns(token);

        // Act
        var result = await _loginHandler.Handle(cmd, CancellationToken.None);

        // Assert
        result.Token.Should().Be(token);
        result.UsuarioId.Should().Be(usuario.Id);
        result.Email.Should().Be(usuario.Email.Valor);
        
    }


    [Fact]
    public async Task Handler_QuandoUsuarioNaoEhEncontrado_DeveLancarDomainException()
    {
        // Arrange
        _usuarioRepositorio
            .Setup(r => r.BuscaPorEmailAsync(_loginTestDataFixture.Email))
            .ReturnsAsync((Usuario?)null);

        // Act
        Func<Task> act = () => _loginHandler.Handle(_loginTestDataFixture.LoginRequest, CancellationToken.None);

        // Assert
        await act.Should()
                 .ThrowAsync<DomainException>()
                 .WithMessage("Credenciais inválidas.");
    }

    [Fact]
    public async Task Handler_QuandoSenhaEhIncorreta_DeveLancarDomainException()
    {
        // Arrange
        var usuario = _loginTestDataFixture.GetUsuario();

        _usuarioRepositorio
            .Setup(r => r.BuscaPorEmailAsync(_loginTestDataFixture.Email))
            .ReturnsAsync(usuario);

        _senhaHasher
            .Setup(h => h.Verify(_loginTestDataFixture.Senha, usuario.SenhaHash))
            .Returns(false);

        // Act
        Func<Task> act = () => _loginHandler.Handle(_loginTestDataFixture.LoginRequest, CancellationToken.None);

        // Assert
        await act.Should()
                 .ThrowAsync<DomainException>()
                 .WithMessage("Credenciais inválidas.");
    }
}

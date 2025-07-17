using Bogus;
using TPCadastroUsuario.Application.Auth.Commands.Login;
using TPCadastroUsuario.Core.Entities;
using TPCadastroUsuario.Core.ValueObjects;

namespace TPCadastroUsuario.Application.Tests.Auth.TestData;

public class LoginCommandTestData
{
    private readonly Faker _faker;

    public string Email { get; }
    public string Senha { get; }
    public LoginCommand LoginRequest { get; }

    public LoginCommandTestData()
    {
        _faker = new Faker("pt_BR");
        Email = _faker.Internet.Email();
        Senha = _faker.Internet.Password();
        LoginRequest = new LoginCommand(Email, Senha);
    }
    public Usuario GetUsuario()
    {
        var nome = _faker.Name.FullName();
        var emailVo = EmailVo.Criar(Email);
        var senhaHash = _faker.Random.AlphaNumeric(32);        
        return new Usuario(nome, emailVo, senhaHash);
    }
}

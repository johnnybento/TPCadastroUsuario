using TPCadastroUsuario.Application.Common.Ports;

namespace TPCadastroUsuario.Adapters.Driven.Infrastructure.Services.Hashing;

public class SenhaHasher : ISenhaHasher
{
    public string Hash(string senha)
        => BCrypt.Net.BCrypt.HashPassword(senha);

    public bool Verify(string senha, string hash)
        => BCrypt.Net.BCrypt.Verify(senha, hash);
}

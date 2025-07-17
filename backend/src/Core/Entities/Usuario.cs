using TPCadastroUsuario.Core.Exceptions;
using TPCadastroUsuario.Core.ValueObjects;

namespace TPCadastroUsuario.Core.Entities;

public class Usuario : EntidadeBase<Guid>
{
    public string Nome { get; private set; }
    public EmailVo Email { get; private set; }
    public string SenhaHash { get; private set; }

    private Usuario() { }

    public Usuario(string nome, EmailVo email,string senhaHash)
    {
        Nome = nome ?? throw new ArgumentNullException(nameof(nome));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        SenhaHash = senhaHash ?? throw new DomainException("Hash da senha inválido.");
    }
}
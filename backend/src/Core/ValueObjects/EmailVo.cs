using System.Text.RegularExpressions;
using TPCadastroUsuario.Core.Exceptions;

namespace TPCadastroUsuario.Core.ValueObjects;

public sealed class EmailVo
{
    private static readonly Regex _emailRegex = new(
          @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
          RegexOptions.Compiled | RegexOptions.IgnoreCase
      );

    public string Valor { get; private set; }
    public EmailVo() { }
    private EmailVo(string valor) => Valor = valor;

    public static EmailVo Criar(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new DomainException("E-mail não pode ser vazio.");

        email = email.Trim().ToLowerInvariant();
        if (!_emailRegex.IsMatch(email))
            throw new DomainException("Formato de e-mail inválido.");

        return new EmailVo(email.Trim().ToLowerInvariant());
    }

    public override bool Equals(object? obj)
    {
        if (obj is not EmailVo outroEmail) return false;
        return Valor == outroEmail.Valor;
    }

    public override int GetHashCode() => Valor.GetHashCode();

    public override string ToString()
    {
        return Valor.ToString();
    }
}
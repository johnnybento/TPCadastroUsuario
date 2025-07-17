using System.Text.RegularExpressions;
using TPCadastroUsuario.Core.Exceptions;

namespace TPCadastroUsuario.Core.ValueObjects;

public sealed class SenhaVo
{
    public string Valor { get; private set; }

    private SenhaVo(string valor) => Valor = valor;

    public static SenhaVo Criar(string senha)
    {
        if (string.IsNullOrWhiteSpace(senha))
            throw new DomainException("Senha não pode ser vazia.");

        if (senha.Length < 8)
            throw new DomainException("Senha deve ter ao menos 8 caracteres.");

        if (!Regex.IsMatch(senha, @"[A-Za-z]"))
            throw new DomainException("Senha deve conter ao menos uma letra.");

        if (!Regex.IsMatch(senha, @"\d"))
            throw new DomainException("Senha deve conter ao menos um número.");

        if (!Regex.IsMatch(senha, @"[!@#$%^&*(),.?""':{}|<>_\-\\/\[\];]"))
            throw new DomainException("Senha deve conter ao menos um caractere especial.");

        return new SenhaVo(senha);
    }

    public override bool Equals(object? obj)
    {
        if (obj is not SenhaVo outraSenha) return false;
        return Valor == outraSenha.Valor;
    }

    public override int GetHashCode() => Valor.GetHashCode();
}

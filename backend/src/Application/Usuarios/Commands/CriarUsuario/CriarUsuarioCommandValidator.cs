using FluentValidation;

namespace TPCadastroUsuario.Application.Usuarios.Commands.CriarUsuario;
public class CriarUsuarioCommandValidator : AbstractValidator<CriarUsuarioCommand>
{
    public CriarUsuarioCommandValidator()
    {
        RuleFor(cmd => cmd.Nome)
            .NotEmpty().WithMessage("Nome é obrigatório.")
            .MaximumLength(100).WithMessage("Nome deve ter até 100 caracteres.");

        RuleFor(cmd => cmd.Email)
            .NotEmpty().WithMessage("Email é obrigatório.")
            .EmailAddress().WithMessage("Email fora do formato válido.");

        RuleFor(cmd => cmd.Senha)
            .NotEmpty().WithMessage("Senha é obrigatória.")
            .MinimumLength(8).WithMessage("Senha deve ter ao menos 8 caracteres.");
    }
}

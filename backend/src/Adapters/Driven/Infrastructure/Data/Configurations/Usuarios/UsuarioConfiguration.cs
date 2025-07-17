using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TPCadastroUsuario.Core.Entities;
using TPCadastroUsuario.Core.ValueObjects;

namespace TPCadastroUsuario.Adapters.Driven.Infrastructure.Data.Configurations.Usuarios;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");
        builder.HasKey(u => u.Id);

        builder.Property(u => u.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
             .HasConversion(
           vo => vo.Valor,
           valor => EmailVo.Criar(valor)

           ).IsRequired()
            .HasMaxLength(200)
            .HasConversion(
                vo => vo.Valor,
                valor => EmailVo.Criar(valor)
            );
        builder.Property(u => u.SenhaHash)
    .IsRequired()
    .HasMaxLength(200);
    }
}

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

        builder.OwnsOne(u => u.Email, vo =>
        {
            vo.Property(e => e.Valor)
                .HasColumnName("Email")
                .IsRequired()
                .HasMaxLength(200);
        });
        builder.Property(u => u.SenhaHash)
    .IsRequired()
    .HasMaxLength(200);
    }
}

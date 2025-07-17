
using AutoMapper;
using TPCadastroUsuario.Application.Usuarios.Commands.CriarUsuario;
using TPCadastroUsuario.Core.Entities;

namespace TPCadastroUsuario.Application.Usuarios.Commands;

public class UsuarioMappingProfile : Profile
{
    public UsuarioMappingProfile()
    {
        CreateMap<Usuario, CriarUsuarioDto>()
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Valor));
    }
}

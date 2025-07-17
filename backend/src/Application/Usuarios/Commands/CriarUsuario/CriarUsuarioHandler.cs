using AutoMapper;
using MediatR;
using TPCadastroUsuario.Application.Common.Ports;
using TPCadastroUsuario.Core.Entities;
using TPCadastroUsuario.Core.Exceptions;
using TPCadastroUsuario.Core.Repositories;
using TPCadastroUsuario.Core.ValueObjects;

namespace TPCadastroUsuario.Application.Usuarios.Commands.CriarUsuario;
public class CriarUsuarioHandler : IRequestHandler<CriarUsuarioCommand, CriarUsuarioDto>
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;
    private readonly ISenhaHasher _passwordHasher;
    private readonly IMapper _mapper;

    public CriarUsuarioHandler(
        IUsuarioRepositorio repo,
        ISenhaHasher passwordHasher,
        IMapper mapper)
    {
        _usuarioRepositorio = repo;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }

    public async Task<CriarUsuarioDto> Handle(CriarUsuarioCommand request, CancellationToken cancellationToken)
    {

        if (await _usuarioRepositorio.VeriricaSeExisteEmailAsync(request.Email))
            throw new DomainException("Email já cadastrado.");


        var emailVo = EmailVo.Criar(request.Email);
        var senhaVo = SenhaVo.Criar(request.Senha);

        var senhaHash = _passwordHasher.Hash(request.Senha);

        var usuario = new Usuario( request.Nome,emailVo,senhaHash);
        

        await _usuarioRepositorio.AddAsync(usuario);

        return _mapper.Map<CriarUsuarioDto>(usuario);
    }
}
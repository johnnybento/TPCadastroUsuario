using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TPCadastroUsuario.Application.Usuarios.Commands.CriarUsuario;
using TPCadastroUsuario.Application.Usuarios.Queries.BuscarUsuarioPorId;
using TPCadastroUsuario.Application.Usuarios.Queries.ListarUsuarios;

namespace TPCadastroUsuario.Adapters.Drivers.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsuariosController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Criar([FromBody] CriarUsuarioCommand comando)
        {
        var usuario = await _mediator.Send(comando);
        return CreatedAtRoute("BuscarPorId", new { id = usuario.Id }, usuario);
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Listar()
    {
        var usuarios = await _mediator.Send(new ListarUsuariosQuery());
        return Ok(usuarios);
    }

    [HttpGet("{id}", Name = "BuscarPorId")]
    [Authorize]
    public async Task<IActionResult> BuscarPorId(Guid id)
    {
        var dto = await _mediator.Send(new BuscarPorIdQuery { Id = id });
        if (dto is null) return NotFound();
        return Ok(dto);
    }
}

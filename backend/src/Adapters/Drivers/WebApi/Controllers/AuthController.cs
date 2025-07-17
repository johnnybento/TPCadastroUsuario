﻿using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TPCadastroUsuario.Application.Auth.Commands.Login;

namespace TPCadastroUsuario.Adapters.Drivers.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginCommand comando)
    {
        var dtoLogin = await _mediator.Send(comando);
        return Ok(dtoLogin);
    }

}

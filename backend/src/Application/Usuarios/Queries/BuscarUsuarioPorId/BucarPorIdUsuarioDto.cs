﻿namespace TPCadastroUsuario.Application.Usuarios.Queries.BuscarUsuarioPorId;
public class BucarPorIdUsuarioDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
}

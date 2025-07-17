using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TPCadastroUsuario.Application.Common.Ports;
using TPCadastroUsuario.Core.Entities;

namespace TPCadastroUsuario.Adapters.Driven.Infrastructure.Services.JWT;

public  class JwtService : IJwtService
{
    private readonly string _chave;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _minutosExpiracao;

    public JwtService(IConfiguration config)
    {
        _chave = config["Jwt:SecretKey"]!;
        _issuer = config["Jwt:Issuer"]!;
        _audience = config["Jwt:Audience"]!;
        _minutosExpiracao = int.Parse(config["Jwt:ExpiresInMinutes"]!);
    }
    public string GenerateToken(Usuario usuario)
    {
        var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_chave));
        var assinatura = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

        var claimParaPaylod = new[]
        {
                new Claim(JwtRegisteredClaimNames.Sub,   usuario.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, usuario.Email.Valor),                
                new Claim(JwtRegisteredClaimNames.Jti,   Guid.NewGuid().ToString())
            };

        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claimParaPaylod,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.AddMinutes(_minutosExpiracao),
            signingCredentials: assinatura
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

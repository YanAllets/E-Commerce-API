using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace ECAPI.AuthenticationService;
public class JwtService
{
    private readonly SymmetricSecurityKey _symmetricKey;

    public JwtService(SymmetricSecurityKey symmetricKey)
    {
        _symmetricKey = symmetricKey;
    }
    public string GenerateToken(int userId)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            //classe com instancia claim ja identificando o nome
        };

        var credentials = new SigningCredentials(_symmetricKey,SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: credentials);
        //ClaimTypes.NameIdentifier
        //tipo da informação: identificação do usuário

        //userId.ToString()
        //valor: identificação convertida em texto
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
} 
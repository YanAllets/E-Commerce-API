using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace ECAPi.AuthenticationService;

public class JwtService
{
    private readonly SymmetricSecurityKey _symmetricKey;

    public JwtService(SymmetricSecurityKey symmetricKey)
    {
        _symmetricKey = symmetricKey;
    }
}
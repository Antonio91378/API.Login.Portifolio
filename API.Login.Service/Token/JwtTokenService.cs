using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Login.Domain.Entities;
using API.Login.Domain.Interfaces.Token;
using API.Login.Utils;
using Microsoft.IdentityModel.Tokens;

namespace API.Login.Service.Token;

public class JwtTokenService : IJwtTokenService
{
    private readonly SymmetricSecurityKey _key;
    public JwtTokenService(IAppConfiguration appConfiguration)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appConfiguration.GetTokenEncodeKey()));
    }
    public string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
        };

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddMinutes(10),
            SigningCredentials = creds
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}

using API.Login.Domain.Entities;

namespace API.Login.Domain.Interfaces.Token;

public interface IJwtTokenService
{
    string CreateToken(User user);
}

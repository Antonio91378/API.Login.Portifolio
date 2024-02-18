namespace API.Login.Domain.Dtos.Response;

public class UserDto
{
    public UserDto(string userName, string token)
    {
        this.UserName = userName;
        this.Token = token;
    }

    public string UserName { get; set; }
    public string Token { get; set; }
}

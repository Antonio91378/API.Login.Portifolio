namespace API.Login.Domain.Dtos.Request;

public class UserRegisterConfirmationDto
{
    public UserRegisterConfirmationDto(string emailHash)
    {
        EmailHash = emailHash;
    }


    public string EmailHash { get; set; }
}

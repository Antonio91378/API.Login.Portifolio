using System.ComponentModel.DataAnnotations;

namespace API.Login.Domain.Dtos.Request;

public class UserLoginDto
{
    public UserLoginDto(string email, string passWord)
    {
        this.Email = email;
        this.PassWord = passWord;
    }

    [Required]
    public string Email { get; set; }
    [Required]
    public string PassWord { get; set; }
}

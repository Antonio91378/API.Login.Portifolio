using System.ComponentModel;

namespace API.Login.Domain.Dtos.Request;

public class UserUpdateDto
{
    [DefaultValue(null)]
    public string? NewUserName { get; set; }

    [DefaultValue(null)]
    public string? NewEmail { get; set; }

    [DefaultValue(null)]
    public string? NewPassWord { get; set; }
}

using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace API.Login.Domain.Dtos.Request;

public class UserRegisterDto(
    string userName,
    string email,
    string passWord,
    string confirmPassWord,
    DateTime birthDay,
    string phone)
{
    [Required]
    [MaxLength(200)]
    public string UserName { get; set; } = userName;

    [Required]
    [MaxLength(200)]
    public string Email { get; set; } = email;

    [Required]
    [MaxLength(20)]
    [DataType(DataType.Password)]
    [Display(Name = "Your PassWord")]
    public string PassWord { get; set; } = passWord;

    [Required]
    [MaxLength(20)]
    [DataType(DataType.Password)]
    [Compare("PassWord")]
    public string ConfirmPassWord { get; set; } = confirmPassWord;


    [Required]
    [DataType(DataType.Date)]
    public DateTime BirthDay { get; set; } = birthDay;

    [Required]
    [DataType(DataType.PhoneNumber)]
    [RegularExpression("^[0-9]{11}$")]
    [StringLength(32)]
    public string Phone { get; set; } = phone;

    [JsonIgnore]
    public byte[]? EmailHash { get; set; }


    [JsonIgnore]
    public byte[]? PassWordHash { get; set; }

    [JsonIgnore]
    public byte[]? PassWordSalt { get; set; }
    public void InitializeComputedPassWordAndHash()
    {
        using var hmac = new HMACSHA512();
        this.PassWordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(this.PassWord));
        this.PassWordSalt = hmac.Key;
    }
}

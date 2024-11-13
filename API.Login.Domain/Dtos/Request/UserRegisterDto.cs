using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace API.Login.Domain.Dtos.Request;

public class UserRegisterDto
{
    public UserRegisterDto(
        string userName,
        string email,
        string passWord,
        string confirmPassWord,
        DateTime birthDay,
        string phone)
    {
        UserName = userName;
        Email = email;
        PassWord = passWord;
        ConfirmPassWord = confirmPassWord;
        BirthDay = birthDay;
        Phone = phone;
        EmailHash = Guid.NewGuid().ToString();
    }

    [Required]
    [MaxLength(200)]
    public string UserName { get; set; }

    [Required]
    [MaxLength(200)]
    public string Email { get; set; }

    [Required]
    [MaxLength(20)]
    [DataType(DataType.Password)]
    [Display(Name = "Your PassWord")]
    public string PassWord { get; set; }

    [Required]
    [MaxLength(20)]
    [DataType(DataType.Password)]
    [Compare("PassWord")]
    public string ConfirmPassWord { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime BirthDay { get; set; }

    [Required]
    [DataType(DataType.PhoneNumber)]
    [RegularExpression("^[0-9]{11}$")]
    [StringLength(32)]
    public string Phone { get; set; }

    [JsonIgnore]
    public string EmailHash { get; set; }

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
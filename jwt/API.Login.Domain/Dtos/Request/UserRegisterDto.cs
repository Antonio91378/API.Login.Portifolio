using System.ComponentModel.DataAnnotations;

namespace API.Login.Domain.Dtos.Request;

public class UserRegisterDto
{
    [Required]
    [MaxLength(200)]
    [Display(Name = "Your name")]
    public string UserName { get; set; }

    [Required]
    [MaxLength(200)]
    [Display(Name = "Your Email")]
    public string Email { get; set; }

    [Required]
    [MaxLength(20)]
    [DataType(DataType.Password)]
    [Display(Name = "Your PassWord")]
    public string PassWord { get; set; }

    [Required]
    [MaxLength(20)]
    [DataType(DataType.Password)]
    [Compare("Password")]
    [Display(Name = "Confirm your password/")]
    public string ConfirmPassWord { get; set; }

    [Required]
    [DataType(DataType.Date)]
    [Display(Name = "Your BirthDay")]
    public DateTime BirthDay { get; set; }

    [DataType(DataType.PhoneNumber)]
    [RegularExpression("^[0-9]{8}$")]
    [StringLength(32)]
    public string Phone { get; set; }
}

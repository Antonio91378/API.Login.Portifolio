using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Login.Domain.Entities.User;

[Table("User")]
public class User
{
    [Key]
    [Required]
    [JsonIgnore]
    [Column("UserId", TypeName = "integer")]
    public int UserId { get; set; }

    [Required]
    [Column("UserName", TypeName = "varchar")]
    [MaxLength(200)]
    public string UserName { get; set; }

    [Required]
    [Column("Email", TypeName = "varchar")]
    [MaxLength(200)]
    public string Email { get; set; }

    [Required]
    [Column("PassWord", TypeName = "varchar")]
    [MaxLength(20)]
    public string PassWord { get; set; }
}

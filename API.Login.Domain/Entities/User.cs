using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Login.Domain.Entities
{
    [Table("User")]
    public class User
    {
        public User(
            string userName,
            string email,
            byte[] passWordSalt,
            byte[] passWordHash)
        {
            UserName = userName;
            Email = email;
            PassWordSalt = passWordSalt;
            PassWordHash = passWordHash;
            Logado = 0;
            Ativo = 0;
        }

        [Key]
        [Required]
        [Column("UserId", TypeName = "integer")]
        public int UserId { get; set; }

        [Column("EmailHash", TypeName = "varchar")]
        [MaxLength]
        public string? EmailHash { get; set; }

        [Required]
        [Column("UserName", TypeName = "varchar")]
        [MaxLength(200)]
        public string UserName { get; set; }

        [Required]
        [Column("Email", TypeName = "varchar")]
        [MaxLength(200)]
        public string Email { get; set; }

        [Required]
        [Column("PassWordSalt", TypeName = "BLOB")]
        [MaxLength(20)]
        public byte[] PassWordSalt { get; set; }

        [Required]
        [Column("PassWordHash", TypeName = "BLOB")]
        [MaxLength]
        public byte[] PassWordHash { get; set; }

        [Column("Logado", TypeName = "int")]
        public int Logado { get; set; }

        [Required]
        [Column("Ativo", TypeName = "int")]
        public int Ativo { get; set; }

        [Column("SessionJwt", TypeName = "varchar")]
        [MaxLength]
        public string? SessionJwt { get; set; }

    }
}

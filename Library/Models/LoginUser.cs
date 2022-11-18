using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.Models
{
    [Table("login_user")]
    public class LoginUser
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [MaxLength(10)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

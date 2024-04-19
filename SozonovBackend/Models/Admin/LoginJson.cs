using System.ComponentModel.DataAnnotations;

namespace SozonovBackend.Models.Admin
{
    public class LoginJson
    {
        [Required(ErrorMessage = "Логин не может быть пустым")]
        [MaxLength(125)]
        public string Login { get; set; } = string.Empty;
        [Required(ErrorMessage = "Пароль не может быть пустым")]
        [MaxLength(25)]
        [MinLength(3)]
        public string Password { get; set; } = string.Empty;
    }
}

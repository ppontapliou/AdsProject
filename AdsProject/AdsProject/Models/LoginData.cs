using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    public class LoginData
    {

        [DisplayName("Логин")]
        [Required(ErrorMessage ="Логин должен быть введён")]
        [StringLength(maximumLength: 30, MinimumLength = 5, ErrorMessage = "Логин должен содержать от 5 aдо 16 символов")]
        public string Login { get; set; }

        [DisplayName("Пароль")]
        [Required(ErrorMessage = "Пароль должен быть введён")]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 16, MinimumLength = 5, ErrorMessage = "Длинна пароля должна быть от 5 aдо 16")]
        public string Password { get; set; }
    }
}

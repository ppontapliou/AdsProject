using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models
{
    public class User
    {
        [DisplayName("Логин")]
        [Required(ErrorMessage = "Логин должен быть введён")]
        [EmailAddress(ErrorMessage = "Логин введён некорректно")]
        [StringLength(maximumLength: 50, MinimumLength = 5,ErrorMessage ="Логин должен содержать от 5 до 50 символов")]
        public string Login { get; set; }

        [DisplayName("Пароль")]
        [Required(ErrorMessage = "Пароль должен быть введён")]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 16, MinimumLength = 5, ErrorMessage = "Длинна пароля должна быть от 5 aдо 16")]
        public string Password { get; set; }

        public int Id { get; set; }

        [DisplayName("Имя пользователя")]
        [Required(ErrorMessage = "Имя пользователя должно быть введено")]
        [StringLength(maximumLength: 100, MinimumLength = 5, ErrorMessage = "Имя должно содержать от 5 до 100 символов")]
        public string UserName { get; set; }

        [DisplayName("Роль")]
        [Required(ErrorMessage = "Роль должна быть введена")]
        public int Role { get; set; }

        [DisplayName("Почта")]
        [Required(ErrorMessage = "Почта должна быть введена")]
        [EmailAddress(ErrorMessage ="Почта введена некорректно")]
        [StringLength(maximumLength: 30, MinimumLength = 5, ErrorMessage = "Почта должна содержать от 5 до 30 символов")]
        public string Mail { get; set; }

        [DisplayName("Телефон")]
        [Required(ErrorMessage = "Телефон должен быть введен")]
        [Phone(ErrorMessage = "Телефон введён некорректно")]
        [StringLength(maximumLength: 15, MinimumLength = 5, ErrorMessage = "Телефон должен содержать от 5 до 15 символов")]
        public string Phone { get; set; }
    }
}

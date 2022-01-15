using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models
{
    public class ChangePasswordModel
    {
        public int Id { get; set; }
        [DisplayName("Пароль")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Пароль должен быть введён")]
        [StringLength(maximumLength: 16, MinimumLength = 5, ErrorMessage = "Длинна пароля должна быть от 5 aдо 16")]
        public string Password { get; set; }

        [DisplayName("Имя пользователя")]
        public string UserName { get; set; }
    }
}

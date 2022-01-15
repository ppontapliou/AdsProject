using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models
{
    
    public class Ad
    {
        public int Id { get; set; }
        [DisplayName("Название")]
        [Required(ErrorMessage = "Название объявления не введено")]
        [StringLength(maximumLength: 100, MinimumLength = 5, ErrorMessage = "Имя должно содержать от 5 до 100 символов")]
        public string Name { get; set; } = "";

        [DisplayName("Описание")]
        [Required(ErrorMessage = "Login is required")]
        public string Title { get; set; }
        [DisplayName("Адресс")]
        [Required(ErrorMessage = "Login is required")]
        [StringLength(maximumLength: 100, MinimumLength = 5, ErrorMessage = "Адрес должен содержать от 5 до 100 символов")]
        public string Address { get; set; }

        public DateTime DateCreation { get; set; }

        public string Image { get; set; }

        public string UserName { get; set; }

        public int UserId { get; set; }
        
        public string Phone { get; set; }

        public string Mail { get; set; }
        [DisplayName("Категория")]
        [Required(ErrorMessage = "Категория должна быть введена")]
        public int Category { get; set; }
        [DisplayName("Состояние")]
        [Required(ErrorMessage = "Состояние должно быть введено")]
        public int State { get; set; }
        [DisplayName("Тип")]
        [Required(ErrorMessage = "Тип должен быть введён")]
        public int Type { get; set; }
    }
}

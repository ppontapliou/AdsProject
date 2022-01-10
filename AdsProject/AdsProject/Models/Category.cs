using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models
{
    public class Category
    {
        public int Id { get; set; }

        [DisplayName("Название")]
        [Required(ErrorMessage = "Категория должна быть введена")]
        [Range(2, 100, ErrorMessage = "Категория должна содержать от 2 до 100 символов")]
        public string Name { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }        
        public string Login { get; set; }       
        public string Password { get; set; }
        public int Role { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
    }
}

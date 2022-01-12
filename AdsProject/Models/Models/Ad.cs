using System;


namespace Models.Models
{
    public class Ad
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Title { get; set; }
        public string Address { get; set; }
        public DateTime DateCreation { get; set; }
        public string Image { get; set; }
        public string UserName { get; set; }
        public int UserId { get; set; }
        public string Phone { get; set; }
        public string Mail { get; set; }
        public int Category { get; set; }
        public int State { get; set; }
        public int Type { get; set; }
    }
}

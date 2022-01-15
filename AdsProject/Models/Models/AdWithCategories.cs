using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Models
{
    public class AdWithCategories
    {
        public Ad Ad { get; set; }
        public List<Parameter> ListCategory { get; set; }
    }
}

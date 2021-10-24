using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Models
{
    public class Position
    {
        private PostalOfficeContext context;

        public int id { get; set; }

        public string position_name { get; set; }
    }
}

using System;

namespace MyKursach2.Models
{
    public class Worker
    {
        private PostalOfficeContext context;

        public int id { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public DateTime date_of_birth { get; set; }

        public string email { get; set; }

        public string position_id { get; set; }

    }
}

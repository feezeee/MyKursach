using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Models
{
    public class Operation
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указана дата рождения")]
        public DateTime DateOfBirth { get; set; }

        public int WorkerId { get; set; }

        public Worker Worker { get; set; }
       

    }
}

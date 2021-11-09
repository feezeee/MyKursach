using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Models
{
    public class Gender
    {
        [Required]
        [Column("gender_id")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Не указан номер телефона")]
        [StringLength(19, ErrorMessage = "Длина строки должна быть до 19 символов")]
        [Remote(action: "CheckGenderName", controller: "Gender", AdditionalFields = "Id", ErrorMessage = "Такой пол уже используется", HttpMethod = "POST")]
        [Column("gender_name")]
        public string GenderName { get; set; }

        public ICollection<Worker> Workers { get; set; }

        public Gender()
        {
            Workers = new List<Worker>();
        }
    }
}

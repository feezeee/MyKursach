using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Models
{
    public class Gender
    {
        [Required]
        public int Id { get; set; }


        [Required(ErrorMessage = "Не указан номер телефона")]
        [StringLength(19, ErrorMessage = "Длина строки должна быть до 19 символов")]
        [Remote(action: "CheckGenderName", controller: "Gender", AdditionalFields = "Id", ErrorMessage = "Такой пол уже используется", HttpMethod = "POST")]
        public string GenderName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostalOffice.Models
{
    public class Register
    {
        public string FirstName { get; set; } = "null";

        public string LastName { get; set; } = "null";

        public DateTime DateOfBirth { get; set; }

        public string? Email { get; set; } 

        public Position Position { get; set; }        

        [Required(ErrorMessage = "Не указан Мобильный телефон")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }
    }
}

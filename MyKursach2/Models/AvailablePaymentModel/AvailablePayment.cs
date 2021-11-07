using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Models
{
    public class AvailablePayment
    {

        [Required]
        public int Id { get; set; }


        [Required(ErrorMessage = "Не указано наименование платежа ")]
        [StringLength(20, ErrorMessage = "Длина строки должна быть до 19 символов")]
        [Remote(action: "CheckAvailablePaymentName", controller: "AvailablePayment", AdditionalFields = "Id", ErrorMessage = "Такое наименование платежа уже используется", HttpMethod = "POST")]
        public string AvailablePaymentName { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Models
{
    public class PaymentMethod
    {

        [Required]

        [Column("paymentmethod_id")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Не указан способ оплаты")]
        [StringLength(19, ErrorMessage = "Длина строки должна быть до 19 символов")]
        [Remote(action: "CheckPaymentMethodName", controller: "PaymentMethod", AdditionalFields = "Id", ErrorMessage = "Такой способ оплаты уже используется", HttpMethod = "POST")]
        [Column("paymentmethod_name")]
        public string PaymentMethodName { get; set; }
    }
}

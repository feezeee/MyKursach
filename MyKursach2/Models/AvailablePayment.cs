using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyKursach2.Models
{
    [Table("available_payments")]
    public class AvailablePayment
    {
        [Column("available_payment_id")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Не указано наименование платежа ")]
        [MaxLength(32)]
        [StringLength(32, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Remote(action: "CheckAvailablePaymentName", controller: "AvailablePayment", AdditionalFields = "Id", ErrorMessage = "Такое наименование платежа уже используется", HttpMethod = "POST")]
        [Column("payment_name")]
        public string AvailablePaymentName { get; set; }


        public virtual List<CompletedPayment> CompletedPayment { get; set; } = new List<CompletedPayment>();
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Models
{
    [Table("payment_methods")]
    public class PaymentMethod
    {
        [Column("payment_method_id")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Не указан способ оплаты")]
        [MaxLength(32)]
        [StringLength(32, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Remote(action: "CheckPaymentMethodName", controller: "PaymentMethod", AdditionalFields = "Id", ErrorMessage = "Такой способ оплаты уже используется", HttpMethod = "POST")]
        [Column("peyment_method_name")]
        public string PaymentMethodName { get; set; }


        public virtual List<CompletedPayment> CompletedPayments { get; set; } = new List<CompletedPayment>();
        public virtual List<DeliveryGood> DeliveryGoods { get; set; } = new List<DeliveryGood>();
        public virtual List<SoldGood> SoldGoods { get; set; } = new List<SoldGood>();

    }
}

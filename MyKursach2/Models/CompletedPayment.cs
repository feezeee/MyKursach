using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Models
{
    [Table("completed_payments")]
    public class CompletedPayment
    {
        [Column("making_payment_id")]
        public int Id { get; set; }


        [Column("operation_id")]
        public int OperationId { get; set; }

        public Operation Operation { get; set; }


        [Column("payment_method_id")]
        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }


        [Column("available_payment_id")]
        public int AvailablePaymentId { get; set; }
        public AvailablePayment AvailablePayment { get; set; }

        [Column("price")]
        public int Price { get; set; }
    }
}

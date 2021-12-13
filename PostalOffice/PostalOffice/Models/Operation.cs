using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostalOffice.Models
{
    [Table("operations")]
    public class Operation
    {
        [Column("operation_id")]
        public int Id { get; set; }


        [Column("operation_date_time")]
        [Required]
        public DateTime? DateTime { get; set; }


        [Column("worker_id")]
        public int WorkerId { get; set; }
        public Worker? Worker { get; set; }


        [Column("total_price")]
        public int TotalPrice { get; set; }



        public virtual List<CompletedPayment> CompletedPayments { get; set; } = new List<CompletedPayment>();
        public virtual List<DeliveryGood> DeliveryGoods { get; set; } = new List<DeliveryGood>();
        public virtual List<SoldGood> SoldGoods { get; set; } = new List<SoldGood>();


        public virtual List<PaymentMethod> PaymentMethods { get; set; } = new List<PaymentMethod>();
        public virtual List<Operation_PaymentMethod> Operations_PaymentMethods { get; set; } = new List<Operation_PaymentMethod>();

    }
}

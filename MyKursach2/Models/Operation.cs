using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyKursach2.Models
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
        public Worker Worker { get; set; }


        [Column("total_price")]
        public int TotalPrice { get; set; }



        public virtual List<CompletedPayment> MakingPayments { get; set; } = new List<CompletedPayment>();

    }
}

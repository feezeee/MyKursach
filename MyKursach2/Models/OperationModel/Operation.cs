using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Models
{
    public class Operation
    {
        [Column("operation_id")]
        public int Id{ get; set; }

        [Column("operation_date_time")]
        public DateTime? DateTime { get; set; }

        [Column("worker_id")]
        public int WorkerId { get; set; }

        public Worker Worker { get; set; }

        public virtual List<MakingPayment> MakingPayments { get; set; } = new List<MakingPayment>();

    }
}

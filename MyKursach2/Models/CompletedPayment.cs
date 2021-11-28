using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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



        [Column("available_payment_id")]
        public int AvailablePaymentId { get; set; }
        public AvailablePayment AvailablePayment { get; set; }

        [Column("payment_price")]
        [Range(0, int.MaxValue, ErrorMessage = "Цена не может быть отрицательной!")]
        public int Price { get; set; }
    }
}

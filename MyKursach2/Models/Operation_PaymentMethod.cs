using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyKursach2.Models
{
    [Table("operations_payment_methods")]
    public class Operation_PaymentMethod
    {
        [Column("operation_id")]
        public int OperationId { get; set; }
        public Operation Operation { get; set; }


        [Column("payment_method_id")]
        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Сумма не может быть отрицательной!")]
        [Column("sum")]
        public int Sum { get; set; }
    }
}

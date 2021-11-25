using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyKursach2.Models
{
    [Table("delivery_goods")]
    public class DeliveryGood
    {
        [Column("delivery_good_id")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Не указан адрес доставки")]
        [MaxLength(32)]
        [StringLength(32, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Column("address_delivery")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Не указано имя отправителя")]
        [MaxLength(32)]
        [StringLength(32, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Column("sender_first_name")]
        public string SenderFirstName { get; set; }


        [Required(ErrorMessage = "Не указана фамилия отправителя")]
        [MaxLength(32)]
        [StringLength(32, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Column("sender_last_name")]
        public string SenderLastName { get; set; }


        [Required(ErrorMessage = "Не указано имя получателя")]
        [MaxLength(32)]
        [StringLength(32, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Column("reciver_first_name")]
        public string ReciverFirstName { get; set; }


        [Required(ErrorMessage = "Не указана фамилия получателя")]
        [MaxLength(32)]
        [StringLength(32, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Column("reciver_last_name")]
        public string ReciverLastName { get; set; }

        [Column("operation_id")]
        public int OperationId { get; set; }
        public Operation Operation { get; set; }


        [Column("payment_method_id")]
        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }


        [Column("country_delivery_id")]
        public int DeliveryCountryId { get; set; }
        public DeliveryCountry DeliveryCountry { get; set; }
    }
}

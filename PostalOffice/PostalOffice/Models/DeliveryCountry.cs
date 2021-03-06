using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostalOffice.Models
{
    [Table("countries_delivery")]
    public class DeliveryCountry
    {

        [Column("country_delivery_id")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Не указана страна доставки")]
        [MaxLength(64)]
        [StringLength(64, ErrorMessage = "Длина строки должна быть до 64 символов")]
        [Remote(action: "CheckDeliveryCountryName", controller: "DeliveryCountry", AdditionalFields = "Id", ErrorMessage = "Такая страна уже используется", HttpMethod = "POST")]
        [Column("country_delivery_name")]
        public string DeliveryCountryName { get; set; }


        public virtual List<DeliveryGood> DeliveryGoods { get; set; } = new List<DeliveryGood>();
    }
}

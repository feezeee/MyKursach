using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Models
{
    public class DeliveryCountry
    {

        [Required]
        [Column("deliverycountry_id")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Не указана страна доставки")]
        [StringLength(20, ErrorMessage = "Длина строки должна быть до 19 символов")]
        [Remote(action: "CheckDeliveryCountryName", controller: "DeliveryCountry", AdditionalFields = "Id", ErrorMessage = "Такая страна уже используется", HttpMethod = "POST")]
        [Column("deliverycountry_name")] 
        public string DeliveryCountryName { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Models
{
    public class Provider
    {
        [Required]
        [Column("provider_id")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Не указан поставщик")]
        [StringLength(19, ErrorMessage = "Длина строки должна быть до 19 символов")]
        [Remote(action: "CheckName", controller: "Provider", AdditionalFields = "Id", ErrorMessage = "Такой поставщик уже существует", HttpMethod = "POST")]
        [Column("provider_name")]
        public string? Name { get; set; }

        [StringLength(30, ErrorMessage = "Длина строки должна быть до 20 символов")]
        [Column("phone_number")]
        public string PhoneNumber { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        public virtual List<GoodForSale> GoodsForSale { get; set; } = new List<GoodForSale>();
        public virtual List<GoodForSale_Provider> GoodsForSale_Providers { get; set; } = new List<GoodForSale_Provider>();

    }
}

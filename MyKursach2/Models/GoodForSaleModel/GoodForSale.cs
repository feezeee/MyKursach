using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Models
{
    public class GoodForSale
    {
        [Required]
        public int Id { get; set; }


        [Required(ErrorMessage = "Не указано имя товара")]
        [StringLength(19, ErrorMessage = "Длина строки должна быть до 19 символов")]
        [Remote(action: "CheckName", controller: "GoodForSale", AdditionalFields = "Id", ErrorMessage = "Такой товар уже существует", HttpMethod = "POST")]
        public string Name { get; set; }

        [Required]
        public int? QuantityInStock { get; set; }

        public virtual ICollection<Provider> Providers { get; set; }

        public GoodForSale()
        {
            Providers = new List<Provider>();
        }
    }
}

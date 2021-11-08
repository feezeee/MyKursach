using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Models
{
    public class Provider
    {
        [Required]
        public int Id { get; set; }


        [Required(ErrorMessage = "Не указан поставщик")]
        [StringLength(19, ErrorMessage = "Длина строки должна быть до 19 символов")]
        [Remote(action: "CheckName", controller: "Provider", AdditionalFields = "Id", ErrorMessage = "Такой поставщик уже существует", HttpMethod = "POST")]
        public string? Name { get; set; }

        [StringLength(30, ErrorMessage = "Длина строки должна быть до 20 символов")]
        public string PhoneNumber { get; set; }

        public string? Email { get; set; }

        public virtual ICollection<GoodForSale> GoodsForSale { get; set; }
        public Provider()
        {
            GoodsForSale = new List<GoodForSale>();
        }
            
    }
}

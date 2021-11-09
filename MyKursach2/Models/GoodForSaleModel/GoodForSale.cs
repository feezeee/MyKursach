using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Models
{
    public class GoodForSale
    {
        [Required]
        [Column("goodsforsale_id")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Не указано имя товара")]
        [StringLength(19, ErrorMessage = "Длина строки должна быть до 19 символов")]
        [Remote(action: "CheckName", controller: "GoodForSale", AdditionalFields = "Id", ErrorMessage = "Такой товар уже существует", HttpMethod = "POST")]

        [Column("goodsforsale_name")] 
        public string Name { get; set; }

        [Required]
        [Column("quantity_in_stock")]
        public int? QuantityInStock { get; set; }

        public virtual ICollection<Provider> Providers { get; set; }

        public GoodForSale()
        {
            Providers = new List<Provider>();
        }

        public List<GoodForSale_Provider> GoodForSale_Providers { get; set; } = new List<GoodForSale_Provider>();
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Models
{
    [Table("goods_for_sale")]
    public class GoodForSale
    {
        [Column("good_for_sale_id")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Не указано имя товара")]
        [MaxLength(32)]
        [StringLength(32, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Remote(action: "CheckName", controller: "GoodForSale", AdditionalFields = "Id", ErrorMessage = "Такой товар уже существует", HttpMethod = "POST")]
        [Column("good_name")]
        public string Name { get; set; }

        [Required]
        [Column("good_amount")]
        public int GoodAmount { get; set; }

        [Required]
        [Column("good_price")]
        public int GoodPrice { get; set; }


        public virtual List<SoldGood> SoldGoods { get; set; } = new List<SoldGood>();
        public virtual List<Provider> Providers { get; set; } = new List<Provider>();
        public virtual List<GoodForSale_Provider> GoodForSale_Providers { get; set; } = new List<GoodForSale_Provider>();



    }
}

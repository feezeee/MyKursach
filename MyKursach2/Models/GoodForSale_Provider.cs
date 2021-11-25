using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Models
{
    [Table("goods_for_sale_providers")]
    public class GoodForSale_Provider
    {
        [Required]
        [Column("good_for_sale_id")]
        public int GoodForSaleId { get; set; }
        public GoodForSale GoodForSale { get; set; }


        [Required]
        [Column("provider_id")]
        public int ProviderId { get; set; }
        public Provider Provider { get; set; }


        [Required]
        [Column("count_good")]
        public int CountGood { get; set; }


        
    }
}

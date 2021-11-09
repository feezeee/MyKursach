using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyKursach2.Models
{
    [Table("goodsforsale_providers")]
    public class GoodForSale_Provider
    {
        [Required]
        [Column("goodforsale_provider_id")]
        public int Id { get; set; }

        [Required]
        [Column("goodforsale_id")]
        public int GoodForSaleId { get; set; }
        public GoodForSale GoodsForSale { get; set; }


        [Required]
        [Column("provider_id")]
        public int ProviderId { get; set; }
        public Provider Providers { get; set; }

    }
}

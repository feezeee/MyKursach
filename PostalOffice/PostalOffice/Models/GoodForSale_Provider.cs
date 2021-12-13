using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostalOffice.Models
{
    [Table("goods_for_sale_providers")]
    public class GoodForSale_Provider
    {
        [Required]
        [Column("good_for_sale_id")]
        public int GoodForSaleId { get; set; }
        public GoodForSale? GoodForSale { get; set; }


        [Required]
        [Column("provider_id")]
        public int ProviderId { get; set; }
        public Provider? Provider { get; set; }


        //[Required]
        //[Column("count_good")]
        //public int CountGood { get; set; }



    }
}

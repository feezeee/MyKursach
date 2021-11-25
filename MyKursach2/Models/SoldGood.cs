using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyKursach2.Models
{
    [Table("sold_goods")]
    public class SoldGood
    {
        [Column("sold_good_id")]
        public int Id { get; set; }


        [Column("number_sold")]
        public int NumberSold { get; set; }



        [Column("operation_id")]
        public int OperationId { get; set; }
        public Operation Operation { get; set; }


        [Column("payment_method_id")]
        public int PaymentMethodId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }


        [Column("good_for_sale_id")]
        public int GoodForSaleId { get; set; }
        public GoodForSale GoodForSale { get; set; }


        [Column("sold_goods_price")]
        public int SoldGoodsPrice { get; set; }
    }
}

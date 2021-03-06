using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PostalOffice.Models
{
    [Table("sold_goods")]
    public class SoldGood
    {
        [Column("sold_good_id")]
        public int Id { get; set; }


        [Column("number_sold")]
        [Range(0,int.MaxValue,ErrorMessage = "Количество товаров не может быть отрицательным!")]
        [Remote(action: "CheckCount", controller: "SoldGood", AdditionalFields = "GoodForSaleId", ErrorMessage = "На складе нет такого количества!", HttpMethod = "POST")]
        public int NumberSold { get; set; }



        [Column("operation_id")]
        public int OperationId { get; set; }
        public Operation? Operation { get; set; }



        [Column("good_for_sale_id")]
        [Required(ErrorMessage = "Не указан товар!")]
        public int GoodForSaleId { get; set; }
        public GoodForSale? GoodForSale { get; set; }


        [Column("sold_goods_price")]
        [Range(0, int.MaxValue, ErrorMessage = "Цена не может быть отрицательной!")]
        public int SoldGoodsPrice { get; set; }
    }
}

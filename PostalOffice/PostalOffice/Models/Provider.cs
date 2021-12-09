using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostalOffice.Models
{
    [Table("providers")]
    public class Provider
    {
        [Column("provider_id")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Не указан поставщик")]
        [MaxLength(32)]
        [StringLength(32, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Remote(action: "CheckName", controller: "Provider", AdditionalFields = "Id", ErrorMessage = "Такой поставщик уже существует", HttpMethod = "POST")]
        [Column("provider_name")]
        public string Name { get; set; }

        [MaxLength(32)]
        [StringLength(32, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Column("provider_phone_number")]
        public string PhoneNumber { get; set; }


        [MaxLength(64)]
        [StringLength(64, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Column("provider_email")]
        public string Email { get; set; }

        public virtual List<GoodForSale> GoodsForSale { get; set; } = new List<GoodForSale>();
        public virtual List<GoodForSale_Provider> GoodsForSale_Providers { get; set; } = new List<GoodForSale_Provider>();

    }
}

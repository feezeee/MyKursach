using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostalOffice.Models
{
    [Table("positions")]
    public class Position
    {
        [Column("position_id")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Заполните поле")]
        [MaxLength(32)]
        [StringLength(32, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Remote(action: "CheckPositionName", controller: "Position", AdditionalFields = "Id", ErrorMessage = "Такая должность уже есть", HttpMethod = "POST")]
        [Column("position_name")]
        public string PositionName { get; set; }

        public ICollection<Worker> Workers { get; set; }

        public Position()
        {
            Workers = new List<Worker>();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using MyKursach2.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyKursach2.Models
{
    public class Position
    {
        [Display(Name = "Number")]
        [Column("position_id")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Заполните поле")]
        [MaxLength(30)]
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

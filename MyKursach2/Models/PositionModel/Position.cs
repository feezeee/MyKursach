using MyKursach2.Data;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyKursach2.Models
{
    public class Position
    {
        [Display(Name = "Number")]
        public int Id { get; set; }


        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(30)]
        public string PositionName { get; set; }

        public ICollection<Worker> Workers { get; set; }
    }
}

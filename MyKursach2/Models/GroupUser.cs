using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyKursach2.Models
{
    [Table("group_users")]
    public class GroupUser
    {
        [Required]
        [Column("group_user_id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Не заданно имя группы пользователей")]
        [MaxLength(32)]
        [StringLength(32, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Remote(action: "CheckName", controller: "GroupUser", AdditionalFields = "Id", ErrorMessage = "Данные группа пользователей уже существуют", HttpMethod = "POST")]
        [Column("group_name")]
        public string Name { get; set; }

        public virtual List<Worker> Worker { get; set; } = new List<Worker>();
    }
}

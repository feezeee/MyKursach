using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyKursach2.Models
{
    [Table("workers")]
    public class Worker
    {
        [Column("worker_id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        [MaxLength(32)]
        [StringLength(32, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Column("worker_first_name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        [MaxLength(32)]
        [StringLength(32, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Column("worker_last_name")]
        public string LastName { get; set; }


        [Required(ErrorMessage = "Не указано отчество")]
        [MaxLength(32)]
        [StringLength(32, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Column("worker_middle_name")]
        public string MiddleName { get; set; }

        [MaxLength(64)]
        [StringLength(64, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Column("worker_email")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Не указана дата рождения")]
        [Column("worker_date_of_birth")]
        public DateTime DateOfBirth { get; set; }


        [Required(ErrorMessage = "Не указан номер телефона")]
        [MaxLength(20)]
        [StringLength(20, ErrorMessage = "Длина строки должна быть до 32 символов")]
        [Remote(action: "CheckPhoneNumber", controller: "Worker", AdditionalFields = "Id", ErrorMessage = "Номер телефона уже используется", HttpMethod = "POST")]
        [RegularExpression(@"[+][0-9]{3}[ ][(][0-9]{2}[)][ ][0-9]{3}[-][0-9]{2}[-][0-9]{2}", ErrorMessage = "Некорректный номер телефона")]
        [Column("worker_phone_number")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Не указан пароль")]
        [MaxLength(64)]
        [StringLength(64, ErrorMessage = "Длина строки должна быть до 32 символов")]

        [Column("worker_password")]
        public string Password { get; set; }



        [Required(ErrorMessage = "Не указана должность")]
        [Column("position_id")]
        public int? PositionId { get; set; }
        public Position Position { get; set; }



        [Required(ErrorMessage = "Не указана группа пользователей")]
        [Column("group_user_id")]
        public int? GroupUserId { get; set; }
        public GroupUser GroupUser { get; set; }



        public List<Operation> Operations { get; set; } = new List<Operation>(); // операции проведенные сотрудником

    }
}

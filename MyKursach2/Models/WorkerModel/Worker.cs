﻿using Microsoft.AspNetCore.Mvc;
using MyKursach2.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace MyKursach2.Models
{
    public class Worker
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        [StringLength(30, ErrorMessage = "Длина строки должна быть до 30 символов")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        [StringLength(30, ErrorMessage = "Длина строки должна быть до 30 символов")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Не указано пол")]
        public int? GenderId { get; set; }

        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Не указана дата рождения")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [StringLength(50, ErrorMessage = "Длина строки должна быть до 50 символов")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Не указан номер телефона")]
        [StringLength(19, ErrorMessage = "Длина строки должна быть до 19 символов")]
        [Remote(action: "CheckPhoneNumber", controller: "Worker", AdditionalFields ="Id", ErrorMessage = "Номер телефона уже используется", HttpMethod = "POST")]
        [RegularExpression(@"[+][0-9]{3}[ ][(][0-9]{2}[)][ ][0-9]{3}[-][0-9]{2}[-][0-9]{2}", ErrorMessage = "Некорректный номер телефона")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Не указана должность")]
        public int? PositionId { get; set; }

        public Position Position { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [StringLength(50, ErrorMessage = "Длина строки должна быть до 50 символов")]
        public string Password { get; set; }

        public virtual ICollection<Operation> Operations { get; set; }
        public Worker()
        {
            Operations = new List<Operation>();
        }

    }
}

using MyKursach2.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace MyKursach2.Models
{
    public class Worker
    {      
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? GenderId { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string? Email { get; set; }

        public string PhoneNumber { get; set; }

        public int? PositionId { get; set; }

        public Position Position { get; set; }

        public string Password { get; set; }
       
    }
}

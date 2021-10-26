using MyKursach2.Data;
using System;
using System.ComponentModel.DataAnnotations;

namespace MyKursach2.Models
{
    public class Worker
    {      
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string? Email { get; set; }


        public int? PositionId { get; set; }

        public Position Position { get; set; }

    }
}

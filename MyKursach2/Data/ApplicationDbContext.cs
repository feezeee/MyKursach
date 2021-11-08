﻿using Microsoft.EntityFrameworkCore;
using MyKursach2.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyKursach2.Data
{


    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }
        public DbSet<Position> Position { get; set; }
        public DbSet<Worker> Worker { get; set; }
        public DbSet<Gender> Gender { get; set; }
        public DbSet<PaymentMethod> PaymentMethod { get; set; }
        public DbSet<DeliveryCountry> DeliveryCountry { get; set; }
        public DbSet<AvailablePayment> AvailablePayment { get; set; }
        public DbSet<GoodForSale> GoodForSale { get; set; }
        public DbSet<Provider> Provider { get; set; }
        public DbSet<Operation> Operation { get; set; }
    }
}

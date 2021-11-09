using Microsoft.EntityFrameworkCore;
using MyKursach2.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyKursach2.Data
{


    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<DeliveryCountry> DeliveryCountries { get; set; }
        public DbSet<AvailablePayment> AvailablePayments { get; set; }
        public DbSet<GoodForSale> GoodsForSale { get; set; }
        public DbSet<Provider> Providers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GoodForSale_Provider>()
            .HasKey(t => new { t.GoodForSaleId, t.ProviderId });

            modelBuilder.Entity<GoodForSale_Provider>()
                .HasOne(pt => pt.GoodsForSale)
                .WithMany(p => p.GoodForSale_Providers)
                .HasForeignKey(pt => pt.GoodForSaleId);

            modelBuilder.Entity<GoodForSale_Provider>()
                .HasOne(pt => pt.Providers)
                .WithMany(t => t.GoodForSale_Providers)
                .HasForeignKey(pt => pt.ProviderId);
        }
    }
}

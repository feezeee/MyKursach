using Microsoft.EntityFrameworkCore;
using MyKursach2.Models;

namespace MyKursach2.Data
{


    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Worker> Workers { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<DeliveryCountry> DeliveryCountries { get; set; }
        public DbSet<AvailablePayment> AvailablePayments { get; set; }
        public DbSet<GoodForSale> GoodsForSale { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<DeliveryGood> DeliveryGoods { get; set; }
        public DbSet<GroupUser> GroupUsers { get; set; }
        public DbSet<SoldGood> SoldGoods { get; set; }
        public DbSet<CompletedPayment> CompletedPayments { get; set; }
        public DbSet<GoodForSale_Provider> GoodForSale_Providers { get; set; }
        public DbSet<Operation_PaymentMethod> Operation_PaymentMethods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
               .Entity<Provider>()
               .HasMany(c => c.GoodsForSale)
               .WithMany(s => s.Providers)
               .UsingEntity<GoodForSale_Provider>(
                  j => j
                   .HasOne(pt => pt.GoodForSale)
                   .WithMany(t => t.GoodForSale_Providers)
                   .HasForeignKey(pt => pt.GoodForSaleId),
               j => j
                   .HasOne(pt => pt.Provider)
                   .WithMany(p => p.GoodsForSale_Providers)
                   .HasForeignKey(pt => pt.ProviderId),
               j =>
               {
                   //j.Property(t => new { t.CountGood }).HasColumnName("count_good").HasColumnType("int").HasDefaultValue(0);
                   j.HasKey(t => new { t.ProviderId, t.GoodForSaleId });
                   j.ToTable("goods_for_sale_providers");
               });


            modelBuilder
               .Entity<Operation>()
               .HasMany(c => c.PaymentMethods)
               .WithMany(s => s.Operations)
               .UsingEntity<Operation_PaymentMethod>(
                  j => j
                   .HasOne(pt => pt.PaymentMethod)
                   .WithMany(t => t.Operations_PaymentMethods)
                   .HasForeignKey(pt => pt.PaymentMethodId),
               j => j
                   .HasOne(pt => pt.Operation)
                   .WithMany(p => p.Operations_PaymentMethods)
                   .HasForeignKey(pt => pt.OperationId),
               j =>
               {
                   //j.Property(t => new { t.CountGood }).HasColumnName("count_good").HasColumnType("int").HasDefaultValue(0);
                   j.HasKey(t => new { t.OperationId, t.PaymentMethodId });
                   j.ToTable("operations_payment_methods");
               });
        }
    }
}

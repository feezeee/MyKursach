using Microsoft.EntityFrameworkCore;

namespace MyKursach2.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        public DbSet<Position> Positions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Контекст получает строку подключения.
            optionsBuilder.UseMySql(Constants.SqlConnectionSQLServer);
        }
    }
}

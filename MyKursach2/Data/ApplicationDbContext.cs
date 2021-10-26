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

    }
}

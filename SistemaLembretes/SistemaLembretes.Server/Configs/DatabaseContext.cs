using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Configs
{
    public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
    {
        public DbSet<Lembrete> Lembretes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }
            optionsBuilder.UseSqlite("Data Source=lembretes.db");
        }
    }
}
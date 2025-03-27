using System.Windows;
using Microsoft.EntityFrameworkCore;
using NotasAPI.Models;

namespace NotasAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            if (Database.GetPendingMigrations().Any())
            {
                Database.Migrate();
            }
        }
        public DbSet<Nota> Notas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Nota>().HasKey(x => x.Id);
            modelBuilder.Entity<Nota>().Property(x => x.Aluno);
            modelBuilder.Entity<Nota>().Property(x => x.Disciplina);
            modelBuilder.Entity<Nota>().Property(x => x.Valor);
            modelBuilder.Entity<Nota>().Property(x => x.DataLancamento);

        }
    }
}

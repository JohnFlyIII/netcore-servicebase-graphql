using SuperInc.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace SuperInc.Infrastructure.Data
{
    public class HeroesDbContext : DbContext
    {
        public HeroesDbContext(DbContextOptions<HeroesDbContext> options)
            : base (options)
        {
        }

        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Sidekick> Sidekicks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
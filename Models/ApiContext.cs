using Microsoft.EntityFrameworkCore;  

namespace BcpChallenge.Models
{
    public class ApiContext : DbContext
    {

        public ApiContext(DbContextOptions<ApiContext> dbContextOptions)
            : base(dbContextOptions) { }

        public DbSet<CurrencyChange> CurrencyChange { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(x => x.IdUser);
            modelBuilder.Entity<CurrencyChange>()
                .HasKey(x => x.IdCurrencyChange);
        }

    }
}
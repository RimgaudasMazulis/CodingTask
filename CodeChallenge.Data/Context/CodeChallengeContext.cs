using CodeChallenge.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeChallenge.Data.Context
{
    public class CodeChallengeContext : DbContext
    {
        public CodeChallengeContext(DbContextOptions<CodeChallengeContext> options) : base(options)
        {
        }

        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Tax> Taxes { get; set; }
        public DbSet<TaxType> TaxTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Municipality>().ToTable("Municipalities");
            modelBuilder.Entity<Tax>().ToTable("Taxes");
            modelBuilder.Entity<TaxType>().ToTable("TaxTypes");
        }
    }
}

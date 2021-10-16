using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using TestExercise.Data.Extensions;
using TestExercise.Models;

namespace TestExercise.Entities
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(DbContextOptions<TestDbContext> options) : base(options)
        { }

        public DbSet<Operator> Operators { get; set; }
        public DbSet<FengShuiNumber> FengShuiNumbers { get; set; }
        public DbSet<PrefixNumbers> PrefixNumbers { get; set; }
        public DbSet<Last2NumCase> Last2NumCases { get; set; }
        public DbSet<MatchCondition> MatchConditions { get; set; }
        public DbSet<BeautyNumber> BeautyNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Operator>(e => {
                e.Property(x => x.ProviderName)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
            });
            modelBuilder.Entity<FengShuiNumber>(e => {
                e.Property(x => x.PhoneNumber)
                .IsRequired()
                .HasMaxLength(10)
                .IsUnicode(false);

                e.Property(x => x.LastNum)
                .HasMaxLength(2)
                .IsUnicode(false);
            });
            modelBuilder.Entity<PrefixNumbers>(e => {
                e.Property(x => x.PrefixNumber)
                .IsRequired()
                .HasMaxLength(3)
                .IsUnicode(false);
            });

            //Data seeding
            modelBuilder.Seed();
        }
    }
}
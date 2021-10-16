using Microsoft.EntityFrameworkCore;
using TestExercise.Models;

namespace TestExercise.Data.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Operator>().HasData(
               new Operator() { Id = 1, ProviderName = "MobiFone" },
               new Operator() { Id = 2, ProviderName = "Vinaphone" },
               new Operator() { Id = 3, ProviderName = "Viettel" }
               );

            modelBuilder.Entity<PrefixNumbers>().HasData(
               new PrefixNumbers() { PrefixId = 1, OperatorId = 1, PrefixNumber = "089" },
               new PrefixNumbers() { PrefixId = 2, OperatorId = 1, PrefixNumber = "090" },
               new PrefixNumbers() { PrefixId = 3, OperatorId = 1, PrefixNumber = "093" },
               new PrefixNumbers() { PrefixId = 4, OperatorId = 2, PrefixNumber = "088" },
               new PrefixNumbers() { PrefixId = 5, OperatorId = 2, PrefixNumber = "091" },
               new PrefixNumbers() { PrefixId = 6, OperatorId = 2, PrefixNumber = "094" },
               new PrefixNumbers() { PrefixId = 7, OperatorId = 3, PrefixNumber = "086" },
               new PrefixNumbers() { PrefixId = 8, OperatorId = 3, PrefixNumber = "096" },
               new PrefixNumbers() { PrefixId = 9, OperatorId = 3, PrefixNumber = "097" }
               );

            modelBuilder.Entity<MatchCondition>().HasData(
               new MatchCondition() { Id = 1, Conditions = "24/29" },
               new MatchCondition() { Id = 2, Conditions = "24/28" }
               );

            modelBuilder.Entity<Last2NumCase>().HasData(
               new Last2NumCase() { Id = 1, chars = "00,66" },
               new Last2NumCase() { Id = 2, chars = "04, 45, 85, 27, 67" },
               new Last2NumCase() { Id = 3, chars = "17, 57, 97, 98, 58" },
               new Last2NumCase() { Id = 4, chars = "42, 82" },
               new Last2NumCase() { Id = 5, chars = "69" }
               );

            modelBuilder.Entity<BeautyNumber>().HasData(
                           new BeautyNumber() { Id = 1, Numbers = "19, 24, 26, 37, 34" }
                           );
        }
    }
}
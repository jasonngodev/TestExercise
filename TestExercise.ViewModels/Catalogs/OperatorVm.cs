using System.Collections.Generic;
using TestExercise.ViewModels.Catalogs;

namespace TestExercise.ViewModels
{
    public class OperatorVm
    {
        public int Id { get; set; }

        public string ProviderName { get; set; }
        public List<PrefixNumbersVm> PrefixNumbers { get; set; }
        public List<FengShuiNumberVm> FengShuiNumbers { get; set; }
    }
}
using System.Collections.Generic;

namespace TestExercise.ViewModels.Catalogs
{
    public class CreateEditPrefixNumbersRequest
    {
        public int PrefixId { get; set; }

        public int OperatorId { get; set; }
        public string PrefixNumber { get; set; }
        public List<OperatorVm> Operators { get; set; }
    }
}
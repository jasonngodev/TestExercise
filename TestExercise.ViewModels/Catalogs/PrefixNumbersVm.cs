namespace TestExercise.ViewModels.Catalogs
{
    public class PrefixNumbersVm
    {
        public int PrefixId { get; set; }

        public int OperatorId { get; set; }
        public string PrefixNumber { get; set; }
        public OperatorVm Operator { get; set; }
    }
}
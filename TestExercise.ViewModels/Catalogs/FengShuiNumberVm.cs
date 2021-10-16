namespace TestExercise.ViewModels.Catalogs
{
    public class FengShuiNumberVm
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string LastNum { get; set; }
        public int OperatorID { get; set; }
        public OperatorVm Operator { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestExercise.Models
{
    public class PrefixNumbers
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PrefixId { get; set; }

        public int OperatorId { get; set; }
        public string PrefixNumber { get; set; }
        public Operator Operator { get; set; }
    }
}
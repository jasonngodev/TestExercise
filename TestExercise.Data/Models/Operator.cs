using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestExercise.Models
{
    public class Operator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string ProviderName { get; set; }
        public List<PrefixNumbers> PrefixNumbers { get; set; }
        public List<FengShuiNumber> FengShuiNumbers { get; set; }
    }
}
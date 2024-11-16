using System.ComponentModel.DataAnnotations;

namespace AIRWIZZ.Data.Entities
{
    public class CurrencyConversion
    {

        [Key, Required]
        public int Currency_Code { get; set; }

        public float Currency_Rate { get; set; }

        public DateTime Last_Updated { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace AIRWIZZ.Data.Entities
{
    public class CurrencyConversion
    {
        [Key, Required]
        public int Currency_Code { get; set; }

        [Required]
        public float ConversionRate { get; set; }

        [Required]
        public DateTime LastUpdated { get; set; }
    }

}

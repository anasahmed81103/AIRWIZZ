using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIRWIZZ.Data.Entities
{
    public class Departure
    {
        [Key, Required]
        public int Departure_id { get; set; }

        public string? Departure_city { get; set; }

        public DateTime Departure_time { get; set;}

        public float Duration {  get; set;}

        public float Price { get; set;}

        [Required , ForeignKey(nameof(Flight))]
        public int Flight_Id { get; set;}


        public virtual required Flight Flight { get; set;}

    }
}

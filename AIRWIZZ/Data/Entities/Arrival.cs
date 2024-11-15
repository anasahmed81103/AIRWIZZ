using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIRWIZZ.Data.Entities
{
    public class Arrival
    {
        [Key,Required]
        public int Arrival_Id {  get; set; }

        public DateTime Arrival_Time { get; set; }

        public string? Arrival_City { get; set; }

        public float duration { get; set; }


        [Required, ForeignKey(nameof(Flight))]
        public int  Flight_Id { get; set; }


        public virtual required Flight Flight { get; set; }

    }
}

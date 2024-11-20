using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIRWIZZ.Data.Entities
{
    public class Departure
    {
        [Key]
        [Required]
        public int Departure_id { get; set; }

        [Required]
        public string DepartureCity { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public float Duration { get; set; }

        [Required]
        public float Price { get; set; }

        // Foreign Key to Flight
        [Required]
        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; }

        // Navigation Property
        public virtual ICollection<Booking> Bookings { get; set; }
    }

}

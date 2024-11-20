using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIRWIZZ.Data.Entities
{
    public class Arrival
    {
        [Key]
        [Required]
        public int Arrival_Id { get; set; }

        [Required]
        public DateTime ArrivalTime { get; set; }

        public string? ArrivalCity { get; set; }

        [Required]
        public float Duration { get; set; }

        // Foreign Key to Flight
        [Required]
        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; }

        // Navigation Property
        public virtual ICollection<Booking>? Bookings { get; set; }
    }

}

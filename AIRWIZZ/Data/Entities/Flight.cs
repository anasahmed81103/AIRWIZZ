using System.ComponentModel.DataAnnotations;

namespace AIRWIZZ.Data.Entities
{
    public class Flight
    {
        [Key]
        [Required]
        public int Flight_Id { get; set; }

        [Required]
        public int FlightNumber { get; set; }

        public string? Airline { get; set; }

        [Required]
        public float TotalPrice { get; set; }

        // Navigation Properties
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Arrival> Arrivals { get; set; }
        public virtual ICollection<Departure> Departures { get; set; }
        public virtual ICollection<SeatPlan> SeatPlans { get; set; }
    }

}

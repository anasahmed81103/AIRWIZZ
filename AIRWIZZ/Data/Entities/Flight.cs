using System.ComponentModel.DataAnnotations;

namespace AIRWIZZ.Data.Entities
{
    public class Flight
    {
        [Key,Required]
        public int Flight_Id {  get; set; }

        public int Flight_Number { get; set; }

        public string? Airline { get; set; }

        public float Total_Price { get; set; }

        public virtual ICollection<Booking> Flights_Bookings { get; set; } 

    }
}

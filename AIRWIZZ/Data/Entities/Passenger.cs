using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIRWIZZ.Data.Entities
{
    public class Passenger
    {
        [Key]
        [Required]
        public int Passenger_Id { get; set; } // Primary Key

        public string? First_Name { get; set; }

        public string? Last_Name { get; set; }

        public string? Passport_Number { get; set; }

        public DateOnly Date_Of_Birth { get; set; }

        public string? Nationality { get; set; }

        // Navigation property for related bookings
        public virtual ICollection<Booking> Passenger_Bookings { get; set; } = new List<Booking>();
    }


}

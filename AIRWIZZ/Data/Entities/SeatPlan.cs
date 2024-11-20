using AIRWIZZ.Data.enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIRWIZZ.Data.Entities
{
    public class SeatPlan
    {
        [Key]
        [Required]
        public int Seat_Id { get; set; }

        [Required]
        public int SeatNumber { get; set; }

        [Required]
        public SeatClass SeatClassType { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; }

        // Navigation Property
        public virtual ICollection<Booking> Bookings { get; set; }
    }

}

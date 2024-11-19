using AIRWIZZ.Data.enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIRWIZZ.Data.Entities
{
    public class Booking
    {
        [Key]
        [Required]
        public int Booking_Id { get; set; } // Primary Key

        public DateTime Booking_Date { get; set; }

        public BookStatus Book_Status_Result { get; set; }

        // Foreign Key to Passenger (Many-to-One Relationship)
        [Required]
        [ForeignKey(nameof(Passenger))]
        public int Passenger_Id { get; set; }

        // Foreign Key to User (Many-to-One Relationship)
        [Required]
        [ForeignKey(nameof(User))]
        public int User_Id { get; set; }

        // Foreign Key to Flight (Many-to-One Relationship)
        [Required]
        [ForeignKey(nameof(Flight))]
        public int Flight_Id { get; set; }

        // Foreign Key to Arrival (Many-to-One Relationship)
        [Required]
        [ForeignKey(nameof(Arrival))]
        public int Arrival_Id { get; set; }

        // Foreign Key to Departure (Many-to-One Relationship)
        [Required]
        [ForeignKey(nameof(Departure))]
        public int Departure_Id { get; set; }

        // Foreign Key to SeatPlan (Many-to-One Relationship)
        [Required]
        [ForeignKey(nameof(SeatPlan))]
        public int Seat_Id { get; set; }

        // Navigation properties
        public virtual Passenger Passenger { get; set; }
        public virtual User User { get; set; }
        public virtual Flight Flight { get; set; }
        public virtual Arrival Arrival { get; set; }
        public virtual Departure Departure { get; set; }
        public virtual SeatPlan SeatPlan { get; set; }
        public virtual Payment Payment { get; set; }
    }


}

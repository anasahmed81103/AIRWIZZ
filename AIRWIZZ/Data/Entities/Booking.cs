using AIRWIZZ.Data.enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security;

namespace AIRWIZZ.Data.Entities
{
    public class Booking
    {
        [Key, Required]
        public int Booking_Id { get; set; }

        public DateTime Booking_Date { get; set; }

        public BookStatus Book_status_result { get; set; }


        [Required, ForeignKey(nameof(Passenger))]
        public int  Passenger_Id { get; set; }


        [Required,ForeignKey(nameof(User))]
        public int  User_id { get; set; }


        [Required, ForeignKey(nameof(Flight))]
        public int  Flight_Id { get; set; }


        [Required, ForeignKey(nameof(SeatPlan))]
       public int Seat_Id { get; set; }





    }
}

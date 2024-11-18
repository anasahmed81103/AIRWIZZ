using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIRWIZZ.Data.Entities
{
    public class Passenger
    {
        public string? First_name { get; set; }

        public string? Last_name { get; set; }

        public string? Passport_Number { get; set; }

        public DateOnly Date_Of_Birth { get; set; }

        public string? Nationality { get; set; }

        [Key,Required]
        public int Passenger_Id { get; set; }

      

        [Required,ForeignKey(nameof(User))]

        public int  User_Id { get; set; }


        public virtual  User  User  { get; set; }

        public virtual ICollection<Booking> Passenger_Bookings { get; set; }


    }
}

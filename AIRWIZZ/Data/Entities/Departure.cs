using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIRWIZZ.Data.Entities
{
    public class Departure
    {
        [Key]
        [Required]
        public int Departure_Id { get; set; }

        public string Departure_City { get; set; }

        public DateTime Departure_Time { get; set; }  // Changed to PascalCase

        public float Duration { get; set; }

        public float Price { get; set; }

        // Foreign Key to Flight (Many-to-One Relationship)
        [Required]
        [ForeignKey(nameof(Flight))]
        public int Flight_Id { get; set; }

        // Navigation properties
        public virtual Flight Flight { get; set; }  // virtual for lazy loading

      public virtual ICollection<Booking> Departure_Bookings { get; set; }
    }
}

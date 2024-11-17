using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIRWIZZ.Data.Entities
{
    public class Arrival
    {
        [Key]
        [Required]
        public int Arrival_Id { get; set; }

        public DateTime Arrival_Time { get; set; }

        public string? Arrival_City { get; set; }

        public float Duration { get; set; }  // It's a good idea to use PascalCase for properties

        // Foreign Key to Flight (Many-to-One Relationship)
        [Required]
        [ForeignKey(nameof(Flight))]
        public int Flight_Id { get; set; }

        
       
        
 
        public virtual Flight Flight { get; set; }  // Use virtual for lazy loading

      public virtual ICollection<Booking>? Arrival_Bookings { get; set; }


    }
}

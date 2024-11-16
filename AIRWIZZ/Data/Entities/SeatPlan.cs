using AIRWIZZ.Data.enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIRWIZZ.Data.Entities
{
    public class SeatPlan
    {

        [Key, Required]
        public int Seat_Id { get; set; }

        public int Seat_Number { get; set; }

        public SeatClass Seat_Class_type { get; set; }   

        public bool Seat_status {  get; set; }

        [Required , ForeignKey(nameof(Flight))]
        public int Flight_Id { get; set; }

        public virtual required Flight Flight { get; set; }


           
    }
}

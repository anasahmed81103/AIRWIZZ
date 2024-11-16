using AIRWIZZ.Data.enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIRWIZZ.Data.Entities
{
    public class Payment
    {
        [Key ,Required]
        public int Payment_Id { get; set; }

        public DateTime Payment_Date { get; set; }

        public float Amount { get; set; }

        public Currency Currency_Type { get; set; }  

        public PaymentMethod Payment_Method_type { get; set; } 

        public PaymentStatus Payment_status_result { get; set; }

        [Required, ForeignKey(nameof(Booking))]
        public int Booking_Id { get; set; }

        [Required, ForeignKey(nameof(User))]
        public int User_Id { get; set; }


        public virtual required User User { get; set; }

        public virtual required Booking Booking { get; set; }


    }
}

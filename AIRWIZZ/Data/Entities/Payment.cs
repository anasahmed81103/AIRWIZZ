using AIRWIZZ.Data.enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AIRWIZZ.Data.Entities
{
    public class Payment
    {
        [Key]
        [Required]
        public int Payment_Id { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; }

        [Required]
        public float Amount { get; set; }

        [Required]
        public Currency CurrencyType { get; set; }

        [Required]
        public PaymentMethod PaymentMethodType { get; set; }

        [Required]
        public PaymentStatus PaymentStatus { get; set; }

        [Required]
        public int BookingId { get; set; }
        public virtual Booking Booking { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }

}

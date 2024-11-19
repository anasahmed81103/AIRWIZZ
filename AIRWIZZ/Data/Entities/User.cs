using AIRWIZZ.Data.enums;
using System.ComponentModel.DataAnnotations;

namespace AIRWIZZ.Data.Entities
{
    public class User
    {
        [Key]
        [Required]
        public int user_id { get; set; }

        public string? user_name { get; set; }

        [Required]
        public string password { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public Role User_role { get; set; }

        [Required]
        public Currency currency_preference { get; set; }

        public DateTime? Date_joined { get; set; }

        // Navigation Property
        public virtual ICollection<Booking> Bookings { get; set; }
    }

}

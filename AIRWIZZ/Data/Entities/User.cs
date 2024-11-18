using AIRWIZZ.Data.enums;
using System.ComponentModel.DataAnnotations;

namespace AIRWIZZ.Data.Entities
{
    public class User
    {
        [Key, Required]
        public int  user_id { get;  set; }
        public string? user_name { get; set; }
        public string? password { get; set; }

        public string? email {  get; set; }

        public Role User_role { get; set; }    

        public Currency currency_preference { get; set; }    

        public DateTime? Date_joined { get; set; }


        public virtual ICollection<Booking> User_Bookings { get; set; }
    }
}

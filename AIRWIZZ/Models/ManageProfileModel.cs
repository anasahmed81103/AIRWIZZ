using AIRWIZZ.Data.enums;
using System.ComponentModel.DataAnnotations;

namespace AIRWIZZ.Models
{
    public class ManageProfileModel
    {

        public string? User_Name { get; set; }
        
        [DataType(DataType.Password)]
        
        public string Password { get; set; }

        public Currency Currency_Preference { get; set; }



    }
}

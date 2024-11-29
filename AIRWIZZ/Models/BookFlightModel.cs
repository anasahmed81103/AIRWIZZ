using AIRWIZZ.Data.enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AIRWIZZ.Models
{
    public class BookFlightModel
    {

        [Required]
        public int passenger_id { get; set; }

        [Required]
        public int seat_id { get; set; }

        [Required]
       public int flight_id { get; set; }

        [Required]
        public string passenger_first_name { get; set; }

        [Required]
        public string passenger_last_name { get; set; }

        [Required]
        public string  passenger_passport_number { get; set; }

        [Required]
        public DateOnly passenger_Date_Of_Birth { get; set; }

        [Required]
        public string  passenger_Nationality { get; set; }

        [Required]
        public SeatClass SeatClassType { get; set; }


        

        [Required]
        public float Amount { get; set; }

        [Required]
        [EnumDataType(typeof(Currency))]
        public Currency CurrencyType { get; set; }


        [Required]
        [EnumDataType(typeof(PaymentMethod))]
        public PaymentMethod PaymentMethodType { get; set; }


       
        [EnumDataType(typeof(PaymentStatus))]
        public PaymentStatus PaymentStatus { get; set; }


        [EnumDataType(typeof(BookStatus))]
        public BookStatus BookStatusResult { get; set; }

        [Required]
        public int seat_num;

        
        


        public List<SelectListItem> FlightList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> SeatList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> DepartureList { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> ArrivalList { get; set; } = new List<SelectListItem>();


    }
}

using AIRWIZZ.Data.enums;

namespace AIRWIZZ.Models
{
    public class BookingHistoryModel
    {
        public List<BookingHistory> Bookings_history_list { get; set; }

        public BookingHistoryModel() { 
        
             this.Bookings_history_list = new List<BookingHistory>();
        
        }


        public class BookingHistory
        {
            public int Bookid { get; set; }

            public DateTime Bookdate { get; set; }

           

            public float total_amount { get; set; }

            public Currency currency_type { get; set; }

           


            public int FlightNumber { get; set; }      // Flight Number
            public string? Airline { get; set; }           // Airline Name
            public string? DepartureLocation { get; set; } // Departure Airport/City
            public string? DestinationLocation { get; set; } // Destination Airport/City
            public DateTime DepartureDateTime { get; set; } // Departure Date and Time
            public DateTime ArrivalDateTime { get; set; }   // Arrival Date and Time

            public string? PassengerName { get; set; }     // Passenger last name
            public SeatClass Seat_class_type { get; set; }         // Seat Class (Economy, Business, First Class)
            public int SeatNum { get; set; }        // Seat Number

            public BookStatus Booking_Status { get; set; }     // Booking Status (Confirmed, Canceled, Pending)


        }

    }
}

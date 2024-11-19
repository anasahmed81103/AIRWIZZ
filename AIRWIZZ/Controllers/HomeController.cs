using AIRWIZZ.Data;
using AIRWIZZ.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AIRWIZZ.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AirwizzContext _airwizzContext;

        public HomeController(ILogger<HomeController> logger, AirwizzContext airwizzContext)
        {
            _logger = logger;
            _airwizzContext = airwizzContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        //public async Task<IActionResult> BookingHistory()
        //{
        //    var user_id = 1;  // user id is done after confimring login page

        //    //var booking_history = await _airwizzContext.Bookings
        //    //        .Include(b => b.Flight)
        //    //        .Include(b => b.Passenger)
        //    //        .Include(b => b.User)
        //    //        .Include(b => b.SeatPlan)
        //          //.Include(b => b.Arrival)            // giving error
        //          //.Include(b => b.Departure)

        //            //.Where(b => b.User_id== user_id)
        //            //.Select(b => new BookingHistoryModel.BookingHistory
        //            //{
        //            //    Bookid = b.Booking_Id,
        //            //    Bookdate = b.Booking_Date,
                        
        //            //    total_amount = b.Flight.Total_Price,
        //            //    currency_type = b.User.currency_preference,
                       
        //            //    FlightNumber = b.Flight.Flight_Number,
        //            //    Airline = b.Flight.Airline,
                       
        //            //    PassengerName = $"{b.Passenger.First_name} {b.Passenger.Last_name}",
        //            //    Seat_class_type = b.SeatPlan.Seat_Class_type,
        //            //    SeatNum = b.SeatPlan.Seat_Number,
        //            //    Booking_Status = b.Book_status_result,

        //                //DepartureDateTime = b.Departure.Departure_Time,
        //                //DepartureLocation = b.Departure.Departure_City,

        //                //ArrivalDateTime = b.Arrival.Arrival_Time,
        //                //DestinationLocation = b.Arrival.Arrival_City



        //            }).ToListAsync();

        //    var model = new BookingHistoryModel
        //    {
        //        Bookings_history_list= booking_history
        //    };




        //    return View(model);

        //}



    }
}

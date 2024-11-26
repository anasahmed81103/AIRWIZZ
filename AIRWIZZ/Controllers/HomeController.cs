using AIRWIZZ.Data;
using AIRWIZZ.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;          // For HttpContext.Session methods (SetInt32, GetInt32, etc.)
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.Identity.Client;
using AIRWIZZ.Data.Entities;

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

			var model = new SearchFlightModel
			{
				arrival_cities = _airwizzContext.Arrivals.Select(a => a.ArrivalCity).ToList(),

				departure_cities = _airwizzContext.Departures.Select(d => d.DepartureCity).ToList(),

			};

			return View(model);

			
        }

        public IActionResult AboutUs()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public async Task<IActionResult> BookingHistory()
        {
            var user_id = HttpContext.Session.GetInt32("UserId");

            var booking_history = await _airwizzContext.Bookings
                    .Include(b => b.Flight)
                    .Include(b => b.Passenger)
                    .Include(b => b.User)
                    .Include(b => b.SeatPlan)
            .Include(b => b.Arrival)            // giving error
            .Include(b => b.Departure)

            .Where(b => b.User_Id == user_id)
            .Select(b => new BookingHistoryModel.BookingHistory
            {
                Bookid = b.Booking_Id,
                Bookdate = b.Booking_Date,

                total_amount = b.Flight.TotalPrice,
                currency_type = b.User.currency_preference,

                FlightNumber = b.Flight.FlightNumber,
                Airline = b.Flight.Airline,

                PassengerName = $"{b.Passenger.First_Name} {b.Passenger.Last_Name}",
                Seat_class_type = b.SeatPlan.SeatClassType,
                SeatNum = b.SeatPlan.SeatNumber,
                Booking_Status = b.Book_Status_Result,

                DepartureDateTime = b.Departure.DepartureTime,
                DepartureLocation = b.Departure.DepartureCity,

                ArrivalDateTime = b.Arrival.ArrivalTime,
                DestinationLocation = b.Arrival.ArrivalCity



            }).ToListAsync();

        var model = new BookingHistoryModel
        {
            Bookings_history_list = booking_history
        };




            return View(model);

    }



        [HttpPost]
        public IActionResult Receive_Fligths_Data(SearchFlightModel searchFlightModel)
        {

            var result_flights = _airwizzContext.Flights.Where(f => f.Arrivals.Any(a => a.ArrivalCity == searchFlightModel.arrival_location)

                                 && f.Departures.Any(d => d.DepartureCity == searchFlightModel.departure_location)).
                                 Where(f => f.Departures.Any(d => d.DepartureTime.Date == searchFlightModel.travel_date)).Include(f=>f.TotalPrice).ToList();

            var model = new ResultModel
            {

                Flights = result_flights,
                

            }; 
            return View(model);
           
            

        }



    }
}

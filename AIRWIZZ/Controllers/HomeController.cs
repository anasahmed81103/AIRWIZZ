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
using AIRWIZZ.Data.enums;
using Microsoft.AspNetCore.Mvc.Rendering;

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


        public IActionResult Book_Flight_Processing()
        {
            var model = new BookFlightModel
            {
                FlightList = _airwizzContext.Flights.Select(f => new SelectListItem
                {
                    Value = f.Flight_Id.ToString(),
                    Text = f.FlightNumber.ToString()
                }
                ).ToList(),

                SeatList = _airwizzContext.SeatPlans.Where(s => s.IsAvailable).Select(s => new SelectListItem
                {
                    Value = s.Seat_Id.ToString(),
                    Text = $"Seat {s.SeatNumber} ({s.SeatClassType})"
                }).ToList(),

                DepartureList = _airwizzContext.Departures.Select(d => new SelectListItem
                {
                    Value = d.Departure_id.ToString(),
                    Text = d.DepartureCity
                }).ToList(),

                ArrivalList = _airwizzContext.Arrivals.Select(a => new SelectListItem
                {
                    Value = a.Arrival_Id.ToString(),
                    Text = a.ArrivalCity
                }).ToList()






            };

            return View(model);
        }




        [HttpPost]
        public IActionResult Book_Flight_Details(BookFlightModel bookFlightModel)
        {

            try
            {
                var model = new Booking
                {
                    Passenger_Id = bookFlightModel.passenger_id,
                    Flight_Id = bookFlightModel.flight_id,
                    Seat_Id = bookFlightModel.seat_id,
                    Booking_Date = DateTime.Now,
                    Book_Status_Result = BookStatus.Booked,
                    Payment = new Payment
                    {
                        PaymentDate = DateTime.Now,
                        Amount = bookFlightModel.Amount,
                        CurrencyType = bookFlightModel.CurrencyType,
                        PaymentMethodType = bookFlightModel.PaymentMethodType,
                        PaymentStatus = PaymentStatus.Successful,

                    },
                    SeatPlan = new SeatPlan
                    {
                        SeatNumber = bookFlightModel.seat_num,
           
                    }
                   

                };

                _airwizzContext.Bookings.Add(model);
                _airwizzContext.SaveChanges();

               
                return View(model);

            }
            catch(Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;

                return View("Error");
                
            }

            
        }





    }
}

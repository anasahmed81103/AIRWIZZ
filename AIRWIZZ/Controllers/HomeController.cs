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

		private float? GetConversionRate(string currencyName)
		{
			// Fetch the existing conversion rates from the database and filter in memory
			var existingRates = _airwizzContext.CurrencyConversions
				.AsEnumerable()  // Load data into memory first
				.Where(x => Enum.IsDefined(typeof(Currency), x.Currency_Code)) // Filter out invalid currencies
				.ToDictionary(
					x => Enum.GetName(typeof(Currency), x.Currency_Code), // Get currency name from Currency_Code
					x => x.ConversionRate
				);

			// Return the conversion rate for the selected currency
			if (existingRates.ContainsKey(currencyName))
			{
				return existingRates[currencyName];
			}

			// If no rate found, return null (or you can default to 1.0 for no conversion)
			return null;
		}



		[HttpPost]
		[Route("Receive_Flights_Data")]
		[Route("Home/Receive_Flights_Data")]
		public IActionResult Receive_Flights_Data(SearchFlightModel searchFlightModel)
		{

			try
			{
				var result_flights = _airwizzContext.Flights.Where(f => f.Arrivals.Any(a => a.ArrivalCity == searchFlightModel.arrival_location)

								&& f.Departures.Any(d => d.DepartureCity == searchFlightModel.departure_location)).
								Where(f => f.Departures.Any(d => d.DepartureTime.Date == searchFlightModel.travel_date)).ToList();


				var preference = searchFlightModel.currency_preference;

				// Fetch the currency conversion rate for the selected currency
				var conversionRate = GetConversionRate(preference);

				// Apply the conversion rate to the total price of each flight
				foreach (var flight in result_flights)
				{
					// Convert the flight price if the currency is set and a valid rate is available
					if (conversionRate.HasValue)
					{
						flight.TotalPrice = flight.TotalPrice / conversionRate.Value; // Apply conversion
					}
				}




				var model = new ResultModel
				{

					Flights = result_flights,
					arrival = searchFlightModel.arrival_location,
					departure = searchFlightModel.departure_location,
					flightDate = searchFlightModel.travel_date,
					preference = searchFlightModel.currency_preference,

				};

				return View(model);
			}
			catch (Exception ex)
			{
				Exception error = ex;
				return RedirectToAction("Index");
			}



		}


		[HttpPost] // Or [HttpGet], based on your form method
		[Route("BookFlight")]
		[Route("Home/BookFlight")]
		public IActionResult BookFlight(int flight_id, string arrival, string departure)
		{

			var flight = _airwizzContext.Flights.FirstOrDefault(f => f.Flight_Id == flight_id);

			if (flight == null)
			{
				return NotFound(); // Handle case if no flight is found
			}

			// Fetch the specific departure and arrival records based on flight_id and city names
			var departureEntity = _airwizzContext.Departures
				.FirstOrDefault(d => d.DepartureCity == departure && d.FlightId == flight_id);

			var arrivalEntity = _airwizzContext.Arrivals
				.FirstOrDefault(a => a.ArrivalCity == arrival && a.FlightId == flight_id);

			if (departureEntity == null || arrivalEntity == null)
			{
				return NotFound(); // Handle case if no matching records are found
			}

			var model = new BookFlightModel
			{

				Flight_Id = flight_id,
				Departure_Id = departureEntity.Departure_id,
				Arrival_Id = arrivalEntity.Arrival_Id,
				Amount = flight.TotalPrice,
				
			};

			return View(model);
		}




		[HttpPost]
		[Route("BookFlightPost")]
		[Route("Home/BookFlightPost")]
		public IActionResult BookFlightPost(BookFlightModel bookFlightModel)
		{
			try
			{
				// Ensure the model has valid values
				if (!ModelState.IsValid)
				{
					throw new Exception("Currency Value not recognized!");
					 // Return to view with validation errors
				}

				int? userId = HttpContext.Session.GetInt32("UserId");


				// Create the Booking object and related entities
				var booking = new Booking
				{
					Flight_Id = bookFlightModel.Flight_Id,
					DepartureId = bookFlightModel.Departure_Id,
					ArrivalId = bookFlightModel.Arrival_Id,
					Booking_Date = DateTime.Now,
					User_Id = (int)userId,
					Book_Status_Result = BookStatus.Booked, // Assuming you want the status to be booked at the time of booking
					Payment = new Payment
					{
						PaymentDate = bookFlightModel.PaymentDate, // You can either use DateTime.Now or the value from the form
						Amount = bookFlightModel.Amount,
						CurrencyType = bookFlightModel.CurrencyType,
						PaymentMethodType = bookFlightModel.PaymentMethodType,
						PaymentStatus = bookFlightModel.PaymentStatus, // Assuming it's selected as 'Pending' or 'Successful'
					},
					SeatPlan = new SeatPlan
					{
						SeatNumber = bookFlightModel.SeatNumber, // Correct seat number mapping
						SeatClassType = bookFlightModel.SeatClassType, // Ensure correct seat class type
						IsAvailable = false, // Assuming the seat is available or taken
						FlightId = bookFlightModel.Flight_Id,
					}
				};

				// Add the booking to the context
				_airwizzContext.Bookings.Add(booking);
				_airwizzContext.SaveChanges(); // Save the changes to the database

				TempData["SuccessMessage"] = "Success.";

				return View(); // Assuming you have a confirmation page
			}
			catch (Exception ex)
			{
				ViewBag.ErrorMessage = ex.Message;
				TempData["ErrorMessage"] = "Fail.";
				// Handle the exception by showing the error message
				return View(); // Redirect to an error page
			}
		}



	}
}

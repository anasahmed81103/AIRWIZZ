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
using System;

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



        [HttpPost]
        [Route("BookFlight")]
        [Route("Home/BookFlight")]
        public IActionResult BookFlight(int flight_id, string arrival, string departure)
        {
            // Fetch the flight details
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

            // Fetch all the booked seat numbers for this flight
            var bookedSeats = _airwizzContext.Bookings
                .Where(b => b.Flight_Id == flight_id && b.Book_Status_Result != BookStatus.Cancelled) // Exclude cancelled bookings
                .Select(b => b.SeatPlan.SeatNumber)
                .ToList();

            // Prepare the model to send to the view
            var model = new BookFlightModel
            {
                Flight_Id = flight_id,
                Departure_Id = departureEntity.Departure_id,
                Arrival_Id = arrivalEntity.Arrival_Id,
                Amount = flight.TotalPrice,
                BookedSeats = bookedSeats, // Pass the list of booked seats
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
                if (!ModelState.IsValid)
                {
                    throw new Exception("Currency Value not recognized!");
                }

                int? userId = HttpContext.Session.GetInt32("UserId");

                // Create and save Passenger
                var passenger = new Passenger
                {
                    First_Name = bookFlightModel.First_Name,
                    Last_Name = bookFlightModel.Last_Name,
                    Passport_Number = bookFlightModel.Passport_Number,
                    Date_Of_Birth = bookFlightModel.Date_Of_Birth,
                    Nationality = bookFlightModel.Nationality
                };
                _airwizzContext.Passengers.Add(passenger);
                _airwizzContext.SaveChanges();

                // Create and save SeatPlan
                var seatPlan = new SeatPlan
                {
                    SeatNumber = bookFlightModel.SeatNumber,
                    SeatClassType = bookFlightModel.SeatClassType,
                    IsAvailable = false,
                    FlightId = bookFlightModel.Flight_Id
                };
                _airwizzContext.SeatPlans.Add(seatPlan);
                _airwizzContext.SaveChanges();

                // Create and save Booking
                var booking = new Booking
                {
                    Flight_Id = bookFlightModel.Flight_Id,
                    DepartureId = bookFlightModel.Departure_Id,
                    ArrivalId = bookFlightModel.Arrival_Id,
                    Booking_Date = DateTime.Now,
                    User_Id = userId.GetValueOrDefault(1),
                    Book_Status_Result = BookStatus.Booked,
                    Passenger_Id = passenger.Passenger_Id,
                    Seat_Id = seatPlan.Seat_Id
                };
                _airwizzContext.Bookings.Add(booking);
                _airwizzContext.SaveChanges();

                // Create and save Payment
                var payment = new Payment
                {
                    PaymentDate = bookFlightModel.PaymentDate,
                    Amount = bookFlightModel.Amount,
                    CurrencyType = bookFlightModel.CurrencyType,
                    PaymentMethodType = bookFlightModel.PaymentMethodType,
                    PaymentStatus = bookFlightModel.PaymentStatus,
                    BookingId = booking.Booking_Id, // Use the generated Booking_Id
                    UserId = userId.GetValueOrDefault(1) // Link the payment to the user
                };
                _airwizzContext.payments.Add(payment);
                _airwizzContext.SaveChanges();

                TempData["SuccessMessage"] = "Booking and Payment successfully processed!";
                return View(); // Confirmation page
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                TempData["ErrorMessage"] = "An error occurred during booking.";
                return View(); // Error page
            }
        }





        [HttpPost]
        public IActionResult CancelBook(int book_id)
        {
            try
            {
                // Find the booking in the database
                var booking = _airwizzContext.Bookings.FirstOrDefault(b => b.Booking_Id == book_id);

                if (booking == null)
                {
                    // Handle case if booking is not found
                    TempData["Error"] = "Booking not found.";
                    return RedirectToAction("BookingHistory");
                }

                // Update booking status to "Canceled" or similar (assuming you have such a status in your enum)
                booking.Book_Status_Result = BookStatus.Cancelled;  // Ensure 'Canceled' is a valid status in your enum

                // Optionally, update the related seat to be available again
                var seatPlan = _airwizzContext.SeatPlans.FirstOrDefault(sp => sp.Seat_Id == booking.Seat_Id);
                if (seatPlan != null)
                {
                    seatPlan.IsAvailable = true;  // Mark the seat as available
                }

                _airwizzContext.SaveChanges();  // Save changes to the database

                TempData["Message"] = "Booking canceled successfully.";  // Success message
            }
            catch (Exception ex)
            {
                // Handle any errors that might occur
                TempData["Error"] = $"An error occurred: {ex.Message}";
            }

            // Redirect the user to the booking history page (or any other appropriate page)
            return RedirectToAction("BookingHistory");
        }



        [HttpPost]
        public IActionResult DeleteBooking(int book_id)
        {
            try
            {
                // Find the booking from the database
                var booking = _airwizzContext.Bookings
                    .Include(b => b.SeatPlan)
                    .Include(b => b.Payment)
                    .Include(b => b.Passenger)
                    .FirstOrDefault(b => b.Booking_Id == book_id);

                // If booking is not found, return an error or redirect
                if (booking == null)
                {
                    TempData["Error"] = "Booking not found.";
                    return RedirectToAction("BookingHistory");
                }

                // Ensure the booking is canceled before deletion
                if (booking.Book_Status_Result != BookStatus.Cancelled)
                {
                    TempData["Error"] = "Only cancelled bookings can be deleted.";
                    return RedirectToAction("BookingHistory");
                }

                // Delete related entities (if necessary, otherwise adjust this part)
                _airwizzContext.SeatPlans.Remove(booking.SeatPlan);
                _airwizzContext.payments.Remove(booking.Payment);
                _airwizzContext.Passengers.Remove(booking.Passenger);

                // Finally, remove the booking
                _airwizzContext.Bookings.Remove(booking);

                // Save changes to the database
                _airwizzContext.SaveChanges();

                TempData["Success"] = "Booking deleted successfully.";
                return RedirectToAction("BookingHistory"); // Redirect to booking history or another page
            }
            catch (Exception ex)
            {
                // Log the error (optional)
                _logger.LogError($"Error deleting booking: {ex.Message}");

                TempData["Error"] = "An error occurred while trying to delete the booking.";
                return RedirectToAction("BookingHistory");
            }
        }











    }
}

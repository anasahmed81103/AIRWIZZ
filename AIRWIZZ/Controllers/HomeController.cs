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
using iTextSharp.text;
using iTextSharp.text.pdf;

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

				
				var conversionRate = GetConversionRate(preference);

				
				foreach (var flight in result_flights)
				{
					
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
            
            var flight = _airwizzContext.Flights.FirstOrDefault(f => f.Flight_Id == flight_id);

            if (flight == null)
            {
                return NotFound(); 
            }

           
            var departureEntity = _airwizzContext.Departures
                .FirstOrDefault(d => d.DepartureCity == departure && d.FlightId == flight_id);

            var arrivalEntity = _airwizzContext.Arrivals
                .FirstOrDefault(a => a.ArrivalCity == arrival && a.FlightId == flight_id);

            if (departureEntity == null || arrivalEntity == null)
            {
                return NotFound(); 
            }

            
            var bookedSeats = _airwizzContext.Bookings
                .Where(b => b.Flight_Id == flight_id && b.Book_Status_Result != BookStatus.Cancelled) // Exclude cancelled bookings
                .Select(b => b.SeatPlan.SeatNumber)
                .ToList();

            
            var model = new BookFlightModel
            {
                Flight_Id = flight_id,
                Departure_Id = departureEntity.Departure_id,
                Arrival_Id = arrivalEntity.Arrival_Id,
                Amount = flight.TotalPrice,
                BookedSeats = bookedSeats, 
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
                //if (!ModelState.IsValid)
                //{
                //    throw new Exception("Currency Value not recognized!");
                //}

                int? userId = HttpContext.Session.GetInt32("UserId");

               
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

                
                var seatPlan = new SeatPlan
                {
                    SeatNumber = bookFlightModel.SeatNumber,
                    SeatClassType = bookFlightModel.SeatClassType,
                    IsAvailable = false,
                    FlightId = bookFlightModel.Flight_Id
                };
                _airwizzContext.SeatPlans.Add(seatPlan);
                _airwizzContext.SaveChanges();

                
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

                
                var payment = new Payment
                {
                    PaymentDate = bookFlightModel.PaymentDate,
                    Amount = bookFlightModel.Amount,
                    CurrencyType = bookFlightModel.CurrencyType,
                    PaymentMethodType = bookFlightModel.PaymentMethodType,
                    PaymentStatus = bookFlightModel.PaymentStatus,
                    BookingId = booking.Booking_Id, 
                    UserId = userId.GetValueOrDefault(1) 
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
                
                var booking = _airwizzContext.Bookings.FirstOrDefault(b => b.Booking_Id == book_id);

                if (booking == null)
                {
                    TempData["Error"] = "Booking not found.";
                    return RedirectToAction("BookingHistory");
                }

                
                booking.Book_Status_Result = BookStatus.Cancelled;  

                
                var seatPlan = _airwizzContext.SeatPlans.FirstOrDefault(sp => sp.Seat_Id == booking.Seat_Id);
                if (seatPlan != null)
                {
                    seatPlan.IsAvailable = true;  // Mark the seat as available
                }

                _airwizzContext.SaveChanges();  

                TempData["Message"] = "Booking canceled successfully."; 
            }
            catch (Exception ex)
            {
                
                TempData["Error"] = $"An error occurred: {ex.Message}";
            }

           
            return RedirectToAction("BookingHistory");
        }



        [HttpPost]
        public IActionResult DeleteBooking(int book_id)
        {
            try
            {
                
                var booking = _airwizzContext.Bookings
                    .Include(b => b.SeatPlan)
                    .Include(b => b.Payment)
                    .Include(b => b.Passenger)
                    .FirstOrDefault(b => b.Booking_Id == book_id);

                
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

                
                _airwizzContext.SeatPlans.Remove(booking.SeatPlan);
                _airwizzContext.payments.Remove(booking.Payment);
                _airwizzContext.Passengers.Remove(booking.Passenger);

              
                _airwizzContext.Bookings.Remove(booking);

                
                _airwizzContext.SaveChanges();

                TempData["Success"] = "Booking deleted successfully.";
                return RedirectToAction("BookingHistory"); 
            }
            catch (Exception ex)
            {
                
                _logger.LogError($"Error deleting booking: {ex.Message}");

                TempData["Error"] = "An error occurred while trying to delete the booking.";
                return RedirectToAction("BookingHistory");
            }
        }



        [HttpPost]
        public FileResult DownloadTicket(int book_id)
        {
           

            var booking  = _airwizzContext.Bookings.Include(b=>b.Passenger)
                .Include(b=>b.Flight).Include(b=>b.Arrival).Include(b=>b.Departure).Include(b=>b.SeatPlan).
               
                FirstOrDefault(b=>b.Booking_Id==book_id);

            if (booking == null)
            {
                throw new Exception ("Booking not found.");
            }

            using (var memoryStream = new MemoryStream())
            {
             
                var document = new Document(PageSize.A4, 36, 36, 36, 36); // Add margins
                PdfWriter.GetInstance(document, memoryStream);

                document.Open();

                // Add a modern title section
                var titleFont = FontFactory.GetFont("Arial", 20, Font.BOLD, BaseColor.BLACK);
                var subTitleFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.GRAY);
                var contentFont = FontFactory.GetFont("Arial", 12, Font.NORMAL, BaseColor.BLACK);
                var boldContentFont = FontFactory.GetFont("Arial", 12, Font.BOLD, BaseColor.BLACK);

                // Header Section
                document.Add(new Paragraph("Flight Booking Ticket", titleFont));
                document.Add(new Paragraph("---------------------------------------------------", subTitleFont));
                document.Add(new Paragraph("AirWizz Airlines", boldContentFont));
                document.Add(new Paragraph("Your reliable travel partner", subTitleFont));
                document.Add(new Paragraph(" ")); // Spacer

                // Main Ticket Content
                PdfPTable table = new PdfPTable(2);
                table.WidthPercentage = 100;
                table.SpacingBefore = 20f;
                table.SpacingAfter = 20f;

                // Styling
                PdfPCell cell = new PdfPCell(new Phrase("Ticket Details"))
                {
                    Colspan = 2,
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Padding = 10,
                    BackgroundColor = BaseColor.LIGHT_GRAY
                };
                table.AddCell(cell);

                // Add booking details
                table.AddCell(new PdfPCell(new Phrase("Booking ID:", boldContentFont)) { Border = Rectangle.NO_BORDER });
                table.AddCell(new PdfPCell(new Phrase($"{booking.Booking_Id}", contentFont)) { Border = Rectangle.NO_BORDER });

                table.AddCell(new PdfPCell(new Phrase("Booking Date:", boldContentFont)) { Border = Rectangle.NO_BORDER });
                table.AddCell(new PdfPCell(new Phrase($"{booking.Booking_Date:dd-MM-yyyy}", contentFont)) { Border = Rectangle.NO_BORDER });

                table.AddCell(new PdfPCell(new Phrase("Passenger Name:", boldContentFont)) { Border = Rectangle.NO_BORDER });
                table.AddCell(new PdfPCell(new Phrase($"{booking.Passenger.First_Name} {booking.Passenger.Last_Name}", contentFont)) { Border = Rectangle.NO_BORDER });

                table.AddCell(new PdfPCell(new Phrase("Flight Number:", boldContentFont)) { Border = Rectangle.NO_BORDER });
                table.AddCell(new PdfPCell(new Phrase($"{booking.Flight.FlightNumber}", contentFont)) { Border = Rectangle.NO_BORDER });

                table.AddCell(new PdfPCell(new Phrase("Airline:", boldContentFont)) { Border = Rectangle.NO_BORDER });
                table.AddCell(new PdfPCell(new Phrase($"{booking.Flight.Airline}", contentFont)) { Border = Rectangle.NO_BORDER });

                table.AddCell(new PdfPCell(new Phrase("Departure Location:", boldContentFont)) { Border = Rectangle.NO_BORDER });
                table.AddCell(new PdfPCell(new Phrase($"{booking.Departure.DepartureCity}", contentFont)) { Border = Rectangle.NO_BORDER });

                table.AddCell(new PdfPCell(new Phrase("Destination Location:", boldContentFont)) { Border = Rectangle.NO_BORDER });
                table.AddCell(new PdfPCell(new Phrase($"{booking.Arrival.ArrivalCity}", contentFont)) { Border = Rectangle.NO_BORDER });

                table.AddCell(new PdfPCell(new Phrase("Departure Date and Time:", boldContentFont)) { Border = Rectangle.NO_BORDER });
                table.AddCell(new PdfPCell(new Phrase($"{booking.Departure.DepartureTime:yyyy-MM-dd HH:mm}", contentFont)) { Border = Rectangle.NO_BORDER });

                table.AddCell(new PdfPCell(new Phrase("Arrival Date and Time:", boldContentFont)) { Border = Rectangle.NO_BORDER });
                table.AddCell(new PdfPCell(new Phrase($"{booking.Arrival.ArrivalTime:yyyy-MM-dd HH:mm}", contentFont)) { Border = Rectangle.NO_BORDER });

                table.AddCell(new PdfPCell(new Phrase("Seat Class:", boldContentFont)) { Border = Rectangle.NO_BORDER });
                table.AddCell(new PdfPCell(new Phrase($"{booking.SeatPlan.SeatClassType}", contentFont)) { Border = Rectangle.NO_BORDER });

                table.AddCell(new PdfPCell(new Phrase("Seat Number:", boldContentFont)) { Border = Rectangle.NO_BORDER });
                table.AddCell(new PdfPCell(new Phrase($"{booking.SeatPlan.SeatNumber}", contentFont)) { Border = Rectangle.NO_BORDER });

                table.AddCell(new PdfPCell(new Phrase("Total Amount:", boldContentFont)) { Border = Rectangle.NO_BORDER });
                table.AddCell(new PdfPCell(new Phrase($"{booking.Flight.TotalPrice} ", contentFont)) { Border = Rectangle.NO_BORDER });

                table.AddCell(new PdfPCell(new Phrase("Booking Status:", boldContentFont)) { Border = Rectangle.NO_BORDER });
                table.AddCell(new PdfPCell(new Phrase($"{booking.Book_Status_Result}", contentFont)) { Border = Rectangle.NO_BORDER });

                // Add table to document
                document.Add(table);

                // Footer
                document.Add(new Paragraph("---------------------------------------------------", subTitleFont));
                document.Add(new Paragraph("Thank you for choosing AirWizz Airlines!", boldContentFont));
                document.Add(new Paragraph("Have a safe and pleasant journey!", subTitleFont));


                // Add content to the PDF


                document.Close();

                // Return the generated PDF
                return File(memoryStream.ToArray(), "application/pdf", $"Ticket_{booking.Booking_Id}.pdf");
            }
           


        }



        public IActionResult chatbot()
        {

            return View();

        }






    }
}

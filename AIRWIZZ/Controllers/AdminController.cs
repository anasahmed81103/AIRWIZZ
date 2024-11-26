using Microsoft.AspNetCore.Mvc;
using AIRWIZZ.Data;
using AIRWIZZ.Data.Entities;
using AIRWIZZ.Data.enums;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AIRWIZZ.Models;

namespace AIRWIZZ.Controllers
{
    public class AdminController : Controller
    {
        private readonly AirwizzContext _airwizzContext;

        public AdminController(AirwizzContext airwizzContext)
        {
            _airwizzContext = airwizzContext;
        }


        public IActionResult mainPortal()
        {
            // Fetch flights with departures and arrivals
            var flights = _airwizzContext.Flights
                .Include(f => f.Departures)
                .Include(f => f.Arrivals)
                .ToList();

            foreach (var flight in flights)
            {
                flight.Departures ??= new List<Departure>();
                flight.Arrivals ??= new List<Arrival>();
            }

            // Fetch bookings
            var bookings = _airwizzContext.Bookings
                .Include(b => b.Flight)
                .Include(b => b.Departure)
                .Include(b => b.Arrival)
                .ToList();

            // Fetch currency conversions and map them to the ViewModel
            var currencyData = _airwizzContext.CurrencyConversions.ToList();
            var currencyRates = currencyData
                .Where(c => c.Currency_Code != (int)Currency.PKR) // Exclude PKR as base
                .Select(c => new CurrencyRate
                {
                    CurrencyPair = $"{((Currency)c.Currency_Code).ToString()} to PKR",
                    Rate = (decimal)c.ConversionRate,
                    LastUpdated = c.LastUpdated
                }).ToList();

            // Combine all data into the ViewModel
            var viewModel = new MainPortalViewModel
            {
                Flights = flights,
                Bookings = bookings,
                CurrencyRates = currencyRates
            };

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult CurrencyConversion()
        {
            // Fetch existing conversion rates from the database and filter in memory
            var existingRates = _airwizzContext.CurrencyConversions
                .AsEnumerable()  // Load data into memory first
                .Where(x => Enum.IsDefined(typeof(Currency), x.Currency_Code)) // Filter out invalid currencies
                .ToDictionary(
                    x => Enum.GetName(typeof(Currency), x.Currency_Code), // Get currency name from Currency_Code
                    x => x.ConversionRate
                );

            // Return to the view with existing rates
            return View(existingRates);
        }



        // POST: Save the conversion rates
        [HttpPost]
        public IActionResult CurrencyConversion(Dictionary<string, float> rates)
        {
            if (rates == null || !rates.Any())
            {
                return View();
            }

            // Loop through each currency conversion rate received from the form
            foreach (var rate in rates)
            {
                // Get the currency code by converting the currency name
                var currencyName = rate.Key;
                var conversionRate = rate.Value;

                // Find the corresponding CurrencyConversion entity in the database
                var currencyCode = Enum.Parse<Currency>(currencyName); // Convert the currency name to the enum
                var currencyConversion = _airwizzContext.CurrencyConversions
                    .FirstOrDefault(c => c.Currency_Code == (int)currencyCode); // Find by the correct Currency_Code

                // If the CurrencyConversion entity exists, update the rate
                if (currencyConversion != null)
                {
                    currencyConversion.ConversionRate = conversionRate;
                    currencyConversion.LastUpdated = DateTime.Now; // Optionally update the LastUpdated timestamp

                    // Save the changes to the database
                    _airwizzContext.SaveChanges();
                }
            }

            return RedirectToAction("CurrencyConversion"); // Redirect back to the GET method to show updated values
        }


        [HttpGet]
        public IActionResult AddFlight()
        {
            return View();
        }

        // POST: Add Flight
        [HttpPost]
        public IActionResult AddFlight(FlightViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Save the main flight
                var flight = new Flight
                {
                    FlightNumber = model.FlightNumber,
                    Airline = model.Airline,
                    TotalPrice = model.TotalPrice
                };

                _airwizzContext.Flights.Add(flight);
                _airwizzContext.SaveChanges();  // Save changes to get the Flight_Id

                // Save arrivals
                foreach (var arrival in model.Arrivals)
                {
                    var flightArrival = new Arrival
                    {
                        FlightId = flight.Flight_Id,  // Correct FlightId assignment
                        ArrivalTime = arrival.ArrivalTime,
                        ArrivalCity = arrival.ArrivalCity,
                        Duration = arrival.ArrivalDuration  // Ensure you are using 'Duration' here as per your model
                    };
                    _airwizzContext.Arrivals.Add(flightArrival);
                }

                // Save departures
                foreach (var departure in model.Departures)
                {
                    var flightDeparture = new Departure
                    {
                        FlightId = flight.Flight_Id,  // Correct FlightId assignment
                        DepartureCity = departure.DepartureCity,
                        DepartureTime = departure.DepartureTime,
                        Duration = departure.DepartureDuration,  // Ensure this matches your model
                        Price = departure.Price  // Make sure 'Price' is included in the ViewModel
                    };
                    _airwizzContext.Departures.Add(flightDeparture);
                }

                // Save all the changes
                _airwizzContext.SaveChanges();

                TempData["SuccessMessage"] = "Flight added successfully!";
                return RedirectToAction("AddFlight");
            }

            return View(model);
        }



        // GET: List Flights
        [HttpGet]
        public IActionResult ViewFlights()
        {
            var flights = _airwizzContext.Flights
                .Include(f => f.Departures)
                .Include(f => f.Arrivals)
                .ToList();

            // Ensure collections are initialized even if empty
            foreach (var flight in flights)
            {
                flight.Departures ??= new List<Departure>();
                flight.Arrivals ??= new List<Arrival>();
            }

            return View(flights);
        }



        [HttpGet]
        public IActionResult ManageFlights()
        {
            var flights = _airwizzContext.Flights.ToList(); // Fetch flights from the database
            return View(flights);
        }



        [HttpPost]
        public IActionResult DeleteFlight(int flightId)
        {
            try
            {
                // Retrieve the flight along with related entities
                var flight = _airwizzContext.Flights
                    .Include(f => f.Arrivals)
                    .Include(f => f.Departures)
                    .Include(f => f.Bookings)
                    .FirstOrDefault(f => f.Flight_Id == flightId);

                if (flight == null)
                {
                    TempData["ErrorMessage"] = "Flight not found!";
                    return RedirectToAction("ManageFlights");
                }

                // Explicitly remove related entities
                if (flight.Arrivals != null)
                {
                    _airwizzContext.Arrivals.RemoveRange(flight.Arrivals);
                }

                if (flight.Departures != null)
                {
                    _airwizzContext.Departures.RemoveRange(flight.Departures);
                }

                if (flight.Bookings != null)
                {
                    _airwizzContext.Bookings.RemoveRange(flight.Bookings);
                }

                // Remove the flight
                _airwizzContext.Flights.Remove(flight);

                // Save changes
                _airwizzContext.SaveChanges();

                TempData["SuccessMessage"] = "Flight and all related data deleted successfully!";
            }
            catch (DbUpdateException dbEx)
            {
                TempData["ErrorMessage"] = "Database error: Unable to delete the flight. Please try again later.";
                Console.WriteLine($"Database error: {dbEx.Message}");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An unexpected error occurred. Please try again.";
                Console.WriteLine($"Error: {ex.Message}");
            }

            return RedirectToAction("ManageFlights");
        }







    }
}

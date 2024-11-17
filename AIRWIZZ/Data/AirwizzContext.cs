using AIRWIZZ.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AIRWIZZ.Data
{
    public class AirwizzContext : DbContext
    {

        public AirwizzContext (   DbContextOptions<AirwizzContext> options ): base( options )
        {



        }

        public DbSet<Arrival> Arrivals { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<CurrencyConversion> CurrencyConversions { get; set; }

        public DbSet<Departure> Departures { get; set; }

        public DbSet<Flight> Flights { get; set; }

        public DbSet<Passenger> Passengers { get; set; }

        public DbSet<Payment> payments { get; set; }

        public DbSet<SeatPlan> SeatPlans { get; set; }

        public DbSet<User> Users { get; set; }

        

    }
}

using AIRWIZZ.Data.Entities;

namespace AIRWIZZ.Models
{

    public class MainPortalViewModel
    {
        public List<Flight> Flights { get; set; }
        public List<Booking> Bookings { get; set; }
        public List<CurrencyRate> CurrencyRates { get; set; }
    }
    public class CurrencyRate
    {
        public string CurrencyPair { get; set; }
        public decimal Rate { get; set; }
        public DateTime LastUpdated { get; set; }
    }

    

}

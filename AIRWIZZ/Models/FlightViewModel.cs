using System;
using System.ComponentModel.DataAnnotations;

namespace AIRWIZZ.Models
{
    public class FlightViewModel
    {
        public int FlightNumber { get; set; }
        public string Airline { get; set; }
        public float TotalPrice { get; set; }
        public List<ArrivalViewModel> Arrivals { get; set; }
        public List<DepartureViewModel> Departures { get; set; }
    }

    public class ArrivalViewModel
    {
        public DateTime ArrivalTime { get; set; }
        public string ArrivalCity { get; set; }
        public float ArrivalDuration { get; set; }
    }

    public class DepartureViewModel
    {
        public DateTime DepartureTime { get; set; }
        public string DepartureCity { get; set; }
        public float DepartureDuration { get; set; }
        public float Price { get; set; }
    }

}

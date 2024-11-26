namespace AIRWIZZ.Models
{
   
    public class FlightAPI
    {
        public List<FlightData> Data { get; set; }
        
    }
   
    public class FlightData
    {
        public string FlightDate { get; set; } // "flight_date"
        public string FlightStatus { get; set; } // "flight_status"
        public FlightDeparture Departure { get; set; } // "departure"
        public FlightArrival Arrival { get; set; } // "arrival"
        public Airline Airline { get; set; } // "airline"
        public FlightDetails Flight { get; set; } // "flight"
        public Aircraft? Aircraft { get; set; } // "aircraft" (null in the example)
        public Live? Live { get; set; } // "live" (null in the example)
    }

    public class FlightDeparture
    {
        public string Airport { get; set; } // "airport"
        public string Timezone { get; set; } // "timezone"
        public string Iata { get; set; } // "iata"
        public string Icao { get; set; } // "icao"
        public string? Terminal { get; set; } // "terminal" (nullable)
        public string? Gate { get; set; } // "gate" (nullable)
        public int? Delay { get; set; } // "delay" (nullable)
        public string Scheduled { get; set; } // "scheduled"
        public string Estimated { get; set; } // "estimated"
        public string Actual { get; set; } // "actual"
        public string EstimatedRunway { get; set; } // "estimated_runway"
        public string ActualRunway { get; set; } // "actual_runway"
    }

    public class FlightArrival
    {
        public string Airport { get; set; } // "airport"
        public string Timezone { get; set; } // "timezone"
        public string Iata { get; set; } // "iata"
        public string Icao { get; set; } // "icao"
        public string? Terminal { get; set; } // "terminal" (nullable)
        public string? Gate { get; set; } // "gate" (nullable)
        public string? Baggage { get; set; } // "baggage" (nullable)
        public int? Delay { get; set; } // "delay" (nullable)
        public string Scheduled { get; set; } // "scheduled"
        public string Estimated { get; set; } // "estimated"
        public string? Actual { get; set; } // "actual" (nullable)
        public string? EstimatedRunway { get; set; } // "estimated_runway" (nullable)
        public string? ActualRunway { get; set; } // "actual_runway" (nullable)
    }

    public class Airline
    {
        public string Name { get; set; } // "name"
        public string Iata { get; set; } // "iata"
        public string Icao { get; set; } // "icao"
    }

    public class FlightDetails
    {
        public string Number { get; set; } // "number"
        public string Iata { get; set; } // "iata"
        public string Icao { get; set; } // "icao"
        public CodeShared Codeshared { get; set; } // "codeshared"
    }

    public class CodeShared
    {
        public string AirlineName { get; set; } // "airline_name"
        public string AirlineIata { get; set; } // "airline_iata"
        public string AirlineIcao { get; set; } // "airline_icao"
        public string FlightNumber { get; set; } // "flight_number"
        public string FlightIata { get; set; } // "flight_iata"
        public string FlightIcao { get; set; } // "flight_icao"
    }

    public class Aircraft
    {
        public string Registration { get; set; } // e.g., "B-KQQ"
        public string Iata { get; set; }        // e.g., "B77W"
        public string Icao { get; set; }        // e.g., "B77W"
        public string Icao24 { get; set; }      // e.g., "780A65"
    }

    public class Live
    {
        public DateTime? Updated { get; set; }      // e.g., "2024-11-25T17:10:41+00:00"
        public double? Latitude { get; set; }      // e.g., 24.2079
        public double? Longitude { get; set; }     // e.g., 111.933
        public double? Altitude { get; set; }      // e.g., 8412.48
        public int? Direction { get; set; }        // e.g., 301
        public double? SpeedHorizontal { get; set; } // e.g., 777.84
        public double? SpeedVertical { get; set; }   // e.g., 0
        public bool IsGround { get; set; }         // e.g., false
    }



}

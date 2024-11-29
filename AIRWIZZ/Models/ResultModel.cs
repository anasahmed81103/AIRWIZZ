using AIRWIZZ.Data.Entities;

namespace AIRWIZZ.Models
{
	public class ResultModel
	{

		public List<Flight> Flights { get; set; }

		public string arrival { get; set; } 

		public string departure { get; set; } 

		public DateTime flightDate { get; set; }

		public string preference { get; set; }



	}
}

using AIRWIZZ.Data.enums;

namespace AIRWIZZ.Models
{
	public class SearchFlightModel
	{

		public DateTime travel_date {  get; set; }

		public string  arrival_location { get; set; }

		public string departure_location { get; set; }

		public string currency_preference { get; set; }


		public List<string>	arrival_cities { get; set; }
		public List<string> departure_cities { get; set; }

	}
}


using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AIRWIZZ.Service
{
    public class FlightTrackingService
    {

        private readonly string apiKey = "c8b3d9510c76333a1050386a2c2dd373";
        
        private readonly string apiUrl = "https://api.aviationstack.com/v1/flights";

        private readonly HttpClient _httpClient;

        public FlightTrackingService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetRealTimeFlightsAsync()
        {

            var url = $"{apiUrl}?access_key={apiKey}";
            
            var response = await _httpClient.GetStringAsync(url);

            Console.WriteLine(response);  // Lo

            return response;
        
        }



    }
}

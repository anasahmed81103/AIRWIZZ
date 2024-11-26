
using System.Net.Http;
using System.Threading.Tasks;
using AIRWIZZ.Data.Entities;
using Newtonsoft.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace AIRWIZZ.Service
{
    public class FlightTrackingService
    {

        private readonly string apiKey = "5cfc45febc2c13cdfb2dedeb21be785b";
        
        private readonly string apiUrl = "https://api.aviationstack.com/v1/flights";

        private readonly HttpClient _httpClient;

        public FlightTrackingService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> GetRealTimeFlightsAsync( )
        {

            
            var url = $"{apiUrl}?access_key={apiKey}";


            var response = await _httpClient.GetStringAsync(url);

          
            Console.WriteLine(response);

            return response;



        }





    }
}

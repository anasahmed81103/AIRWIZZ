using AIRWIZZ.Service;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using AIRWIZZ.Services;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AIRWIZZ.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
//using AIRWIZZ.Models.Registration;
//using AIRWIZZ.Data.Store;
//using AIRWIZZ.Data;
using Microsoft.AspNetCore.Identity;
//using AIRWIZZ.Data.Entities;
using System.Threading;
//using AIRWIZZ.Data.Enums;
using Microsoft.VisualBasic;
using Microsoft.EntityFrameworkCore;
//using AIRWIZZ.Models.Login;
using System.Linq.Expressions;
using Microsoft.Identity.Client;
using System.Reflection;
using AIRWIZZ.Data;
using AIRWIZZ.Data.Entities;
using AIRWIZZ.Data.enums;
using Microsoft.AspNetCore.Http;          // For HttpContext.Session methods (SetInt32, GetInt32, etc.)
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Newtonsoft.Json; // For configuring session services in Program.cs or Startup.cs
				   //using AIRWIZZ.Services.Caching;


namespace AIRWIZZ.Controllers
{
    public class FlightTrackingController : Controller
    {

        private readonly FlightTrackingService _flightTrackingService;

        public FlightTrackingController(FlightTrackingService flightTrackingService)
        {
            
            _flightTrackingService = flightTrackingService;

        }


        [HttpGet]
        [Route("GetFlightsData")]
        public async Task<IActionResult> GetFlightsData()
        {
            try
            {
                var flightData = await _flightTrackingService.GetRealTimeFlightsAsync();

                var rawData = JsonConvert.DeserializeObject<FlightAPI>(flightData);

                
            

                return View(rawData);

            }


            catch (Exception ex)
            {
                // Handle errors gracefully and return an error response
               ViewBag.ErrorMessage = ex.Message;

                return View("Error");

            }




        }




       // [HttpGet]

        //public IActionResult RealTimeFlightMap()
        //{
        //    return View();
        //}




    }
}

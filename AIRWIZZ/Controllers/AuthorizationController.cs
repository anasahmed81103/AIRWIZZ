﻿using Microsoft.AspNetCore.Mvc;
//using AIRWIZZ.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
using Microsoft.Extensions.DependencyInjection; // For configuring session services in Program.cs or Startup.cs
//using AIRWIZZ.Services.Caching;



namespace AIRWIZZ.Controllers
{
    public class AuthorizationController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AirwizzContext _airwizzContext;

        public AuthorizationController( AirwizzContext airwizzContext)
        {
            _airwizzContext = airwizzContext;
        }

        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        [Route("Login")]
        public async Task<IActionResult> Login()
        {
            return View();
        }


        [Route("Signup")]
        public async Task<IActionResult> Signup()
        {
            return View();
        }



        [HttpPost]
        [Route("SigninUser")]
        public async Task<IActionResult> SigninUser(string name, string email, string password, string currency)
        {
            try
            {

                Currency parsedCurrency;

                // Attempt to parse the string to the Currency enum
                bool isValidCurrency = Enum.TryParse<Currency>(currency, true, out parsedCurrency);

                if (!isValidCurrency)
                {
                    throw new Exception("Currency Value not recognized!");
                }

                var newUser = new User
                {
                    user_name = name,
                    email = email,
                    password = password, 
                    currency_preference = parsedCurrency,
                    User_role = Role.naive_user, 
                    Date_joined = DateTime.Now
                };

               
                await _airwizzContext.Users.AddAsync(newUser); 
                await _airwizzContext.SaveChangesAsync();

                //_logger.LogInformation($"New user registered: {email}");

                TempData["SuccessMessage"] = "User registered successfully! Please log in.";

                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error occurred during user signup.");
                TempData["ErrorMessage"] = "An error occurred while processing your request. Please try again later.";
                return RedirectToAction("Signup");
            }
        }




        [HttpPost]
        [Route("LoginUser")]
        public async Task<IActionResult> LoginUser (string email, string password)
        {
            try
            {

                var user = await _airwizzContext.Users.Where(x => x.email == email).FirstOrDefaultAsync();

                if (user == null)
                {
                    throw new Exception("No such User!");
                }
                else if (user.password != password)
                {
                    throw new Exception("Wrong Password!");
                }

                HttpContext.Session.SetInt32("UserId", user.user_id);
                HttpContext.Session.SetString("UserName", user.email);

                //TempData["SuccessMessage"] = "Logged in successfully!";

                return RedirectToAction("Index");

            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error occurred during user signup.");
                TempData["ErrorMessage"] = "Login Error!";
                return RedirectToAction("Login");
            }
        }


        [Route("Logout")]
        public IActionResult Logout()
        {
            // Clear the session
            HttpContext.Session.Clear();

            TempData["SuccessMessage"] = "Logged out successfully!";
            return RedirectToAction("Login");
        }


        //var userId = HttpContext.Session.GetInt32("UserId");   for ibrahim bhai



        //[Route("Logout")]
        //public async Task<IActionResult> Logout()
        //{

        //    LogoutAndClear();
        //    TempData["alert"] = "You Have been Logged Out!";
        //    return RedirectToAction("Index", "Home");

        //}


        //private void LogoutAndClear()
        //{
        //    HttpContext.Session.Remove("SessionId");
        //    HttpContext.Session.Remove("UserName");
        //    HttpContext.Session.Remove("Roles");
        //    HttpContext.Session.Remove("LoggedId");

        //}


    }
}

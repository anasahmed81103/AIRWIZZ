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
using AIRWIZZ.Data.enums;
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
using System.Security.Claims; // For configuring session services in Program.cs or Startup.cs
//using AIRWIZZ.Services.Caching;
using System.Net.Mail;
using System.Net;



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
                else if (await _airwizzContext.Users.Where(x => x.email == email).FirstOrDefaultAsync() != null)
                {
                    TempData["ErrorMessage"] = "This Email is already Used !";
                    return RedirectToAction("Signup");
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

                //await SendRegistrationEmail(email, name);

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



        // Helper method to send email
        private async Task SendRegistrationEmail(string email, string name)
        {
            try
            {
                var fromAddress = new MailAddress("airwizzdeveloper@gmail.com", "AirWizz");
                var toAddress = new MailAddress(email, name);
                const string fromPassword = "airwizzadmin123"; // Use a secure method to store passwords
                const string subject = "Welcome to Our Website!";
                string body = $"Hi {name},\n\nThank you for registering at our site. We are excited to have you onboard!\n\nBest regards,\nAnas Ahmed, Ibrahim Junaid & ZainM";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com", // Replace with your SMTP host
                    Port = 587,
                    EnableSsl = true,
                    //DeliveryMethod = SmtpDeliveryMethod.Network,
                    //UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                };

                await smtp.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                // Log email error
                //_logger.LogError(ex, "Error occurred while sending registration email.");
                throw;
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
                if (user.User_role == Role.admin)
                {
                    HttpContext.Session.SetInt32("UserId", user.user_id);
                    HttpContext.Session.SetString("UserName", user.email);
                    HttpContext.Session.SetString("Role", "admin");

                    return RedirectToAction("mainPortal", "Admin");
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


        [Route("ManageProfile")]
        public async Task<IActionResult> ManageProfile()
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");

                var user = await _airwizzContext.Users.Where(x => x.user_id == userId).FirstOrDefaultAsync();

                if (user == null)
                {
                    throw new Exception("No such User!");
                }

                var user_data = new ManageProfileModel
                {
                    User_Name = user.user_name,
                    Password = user.password,
                    Currency = user.currency_preference.ToString() // Ensure this is enum, or just store it as a string if needed.
                };

                return View(user_data);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Login Error!";
                return RedirectToAction("Login");
            }
        }




        [HttpPost]
        [Route("ManageProfilePost")]
        public async Task<IActionResult> ManageProfilePost(string User_Name, string Password, string Currency)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");

                var user = await _airwizzContext.Users.Where(x => x.user_id == userId).FirstOrDefaultAsync();

                if (user == null)
                {
                    throw new Exception("No such User!");
                }

                // Update user fields
                user.user_name = User_Name;
                user.password = Password;

                // Parse the selected currency and assign it to the user
                Currency parsedCurrency;
                bool isValidCurrency = Enum.TryParse(Currency, out parsedCurrency); // parse to enum
                if (isValidCurrency)
                {
                    user.currency_preference = parsedCurrency;
                }
                else
                {
                    // If invalid, set a default or handle the error
                    TempData["ErrorMessage"] = "Invalid Currency Selection.";
                    return RedirectToAction("ManageProfile");
                }

                // Save the changes
                await _airwizzContext.SaveChangesAsync();

                TempData["SuccessMessage"] = "Data Successfully Updated!";
                return RedirectToAction("ManageProfile");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Error occurred while updating profile.";
                return RedirectToAction("ManageProfile");
            }
        }










    }
}

            





        




    


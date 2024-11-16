using Microsoft.AspNetCore.Mvc;
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
//using AIRWIZZ.Services.Caching;



namespace AIRWIZZ.Controllers
{
    public class AuthorizationController : Controller
    {
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



        //[HttpPost]
        //[Route("LoginUser")]
        //public async Task<IActionResult> LoginUser(IFormCollection collection)
        //{

        //    var userName = collection["email"];
        //    var password = collection["password"];

        //    try
        //    {
        //        var checkUser = _context.GameReviews.Any(x => x.Name == userName);

        //        if (checkUser != null)
        //        {
        //            if (userAccess.IsVerified)
        //            {
        //                var userRoles = await _dataContext.UserRoles.Include(x => x.Role)
        //                    .Where(x => x.UserId == checkUser.Id)
        //                    .Select(x => x.Role.Name.ToLower())
        //                    .ToListAsync();

        //                SetLoginValues(checkUser.Id, checkUser.UID,
        //                    checkUser.Email, userAccess.SessionId, userRoles);
        //                _cache.SetUserBySessionId(new Models.Cache.CacheUser()
        //                {
        //                    Id = checkUser.Id,
        //                    SessionId = userAccess.SessionId,
        //                    UID = checkUser.UID,
        //                    Roles = userRoles
        //                }, userAccess.SessionId);
        //                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        //            }
        //        }
        //        throw new Exception("Login failed!");
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["error"] = "Login Failed!";
        //        return RedirectToAction("Login", "Auth");
        //    }
        //}



        //[Route("Logout")]
        //public async Task<IActionResult> Logout()
        //{

        //    LogoutAndClear();
        //    TempData["alert"] = "You Have been Logged Out!";
        //    return RedirectToAction("Index", "Home");

        //}




        //// POST: AuthController/Create
        //[HttpPost]
        //// [ValidateAntiForgeryToken]
        //[Route("CreateUser")]
        //public async Task<IActionResult> CreateUser(IFormCollection collection)
        //{


        //    try
        //    {

        //        string email = collection["email"];

        //        if (await _userStore.FindByNameAsync(email, cts.Token) == null)
        //        {

        //            //_userStore.FindByNameAsync("email", cts.Token);

        //            var salt = _configuration.GetValue<long>("Settings:IntSalt");
        //            //  put in values of each property in below line


        //            string pass = collection["password"];
        //            string name = collection["fullName"];
        //            string date = collection["dob"];
        //            string gen = collection["Gender"];

        //            DateTime d = DateTime.Now;
        //            DateTime.TryParse(date, out d);
        //            Gender gend = ConvertToGender(gen);

        //            UserModel newUser = new UserModel(email, pass, name, d, gend, salt);


        //            var normalRole = await _roleStore.FindByNameAsync("normalUser", cts.Token);

        //            User toRegister = new User()
        //            {
        //                DateOfBirth = newUser.DateOfBirth,
        //                Email = newUser.Email,
        //                FullName = newUser.FullName,
        //                Gender = newUser.Gender,
        //                PasswordHash = newUser.PasswordHash,
        //                UID = newUser.UID,
        //            };

        //            await _dataContext.Users.AddAsync(toRegister);

        //            await _dataContext.UserRoles.AddAsync(new UserRole()
        //            {
        //                Role = normalRole,
        //                User = toRegister
        //            });
        //            await _dataContext.SaveChangesAsync();


        //            TempData["success"] = " ID Succesfully created. Login Now!";


        //            return RedirectToAction("Login", "Auth");


        //        }
        //        else
        //        {
        //            throw new Exception("Username Exists!");
        //        }


        //    }

        //    catch (Exception ex)
        //    {
        //        TempData["error"] = " Someting went Wrong. Try Again!";

        //        return RedirectToAction("Login", "Auth");

        //    }


        //}


        //private void SetLoginValues(int userId, Guid UID, string email, string sessionId, List<string> roles)
        //{
        //    var name = email.Split("@")[0];
        //    HttpContext.Session.SetString("SessionId", sessionId);
        //    HttpContext.Session.SetString("UserName", name);
        //    HttpContext.Session.SetString("Roles", string.Join(", ", roles));
        //    HttpContext.Session.SetInt32("LoggedId", 1);
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

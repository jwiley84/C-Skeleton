using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using skeleton.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace skeleton.Controllers
{
    public class UserController : Controller
    {
        private skeletonContext _context;
        
        public UserController(skeletonContext context)
        {
            _context = context;
        }
        [HttpGet] //this shows the form to login
        [Route("")]
        public IActionResult Index() 
        {
            return View("Index");
        }

        [HttpPost] //this sends the data for validation
        [Route("Login")]
        public IActionResult Login(string login_email, string login_password)
        {
            User foundUser = _context.users.SingleOrDefault(user => user.email == login_email);
            var Hasher = new PasswordHasher<User>();
            if (foundUser == null || 0 == Hasher.VerifyHashedPassword(foundUser, foundUser.password, login_password) ) 
            {
                ViewBag.errorMsg = "Invalid login. Please check email or password and try again.";
                return View("Index");
            }
            else
            {
                HttpContext.Session.SetInt32("userID", foundUser.userID);
                TempData["String"] = "Login";
                // !return Redirect("/GameSession/Splash");
                return RedirectToAction("Splash", "Activity");
            }
            
        }

        // [HttpGet]
        // [Route("user/edit")]
        // public IActionResult Edit()
        // {
        //     !return Content("userID = 0");
        // }
        
        [HttpGet] // this shows the form for register
        [Route("user/create")]
        public IActionResult Create()
        {
            return View("Create");
        }

        [HttpPost] // this posts the values to the DB for register
        [Route("Register")]
        public IActionResult Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                User oldUser = _context.users.SingleOrDefault(user => user.email == model.email);
                if (oldUser != null)
                {
                    ViewBag.errorMsg = "This email is already registered! Please go back and log in instead!";
                    return View ("Create", model);
                }
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                User newUser = new User 
                {
                    firstName = model.firstName,
                    lastName = model.lastName,
                    email = model.email,
                    password = model.password,
                    created_at = DateTime.UtcNow,
                    updated_at = DateTime.UtcNow
                };
                newUser.password = Hasher.HashPassword(newUser, newUser.password);
                _context.Add(newUser);
                _context.SaveChanges();
                newUser = _context.users.SingleOrDefault(user => user.email == newUser.email);
                HttpContext.Session.SetInt32("userID", newUser.userID);
                
                //need to add confirmation for matching passwords
                TempData["String"] = "Valid Registration"; 
                return RedirectToAction("Splash", "Activity");
            }
            else 
            {
                return View("Create", model);
            }
        }
        

    }
}
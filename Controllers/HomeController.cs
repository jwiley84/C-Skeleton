using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using skeleton.Models;

namespace skeleton.Controllers
{
    public class HomeController : Controller
    {
        

        private skeletonContext _context;

        public HomeController(skeletonContext context)
        {
         _context = context;

        }
        [HttpGet]
        [Route("home/index")]
        public IActionResult Index()
        {   
            List<User> AllUsers = _context.users.ToList();
            ViewBag.AllUsers = AllUsers;
            return View();
        }
        // public IActionResult Error()
        // {
        //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        // }
    }
}

using Microsoft.AspNetCore.Mvc;
using skeleton.Models;
using System;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace skeleton.Controllers
{
    public class ActivityController : Controller
    {

        private skeletonContext _context;

        public ActivityController(skeletonContext context)
        {
         _context = context;
        }

// show list of game sessions
        [HttpGet]
        [Route("Activity/Splash")]
        public IActionResult Splash()
        {
            int? userID = HttpContext.Session.GetInt32("userID");
            if (userID != null)
            {
                List<Activity> AllActivitys = _context.activities
                  .Include(GS => GS.user)
                  .Include(GS => GS.attendeesList)
                  .ThenInclude(attendees => attendees.user).ToList();
                List<Dictionary<string, object>> ActivityList = new List<Dictionary<string, object>>();
                foreach (Activity activities in AllActivitys)
                {
                  bool owned = false;
                  bool RSVPed = false;
                  int attendees = 0;
                  string dA = "";
                  User ownerUser = _context.users.SingleOrDefault(user => user.userID == activities.userID);
                  string owner = ownerUser.firstName;
                //   string owner = activities.userID.firstName;
                  if (HttpContext.Session.GetInt32("userID") == activities.userID)
                  {
                    owned = true;

                  }
                  if (activities.durationAnnotation == 1) {
                      dA = "Days";
                  }
                  else if (activities.durationAnnotation == 2) {
                      dA = "Hours";
                  }
                  else if (activities.durationAnnotation ==  3) {
                      dA = "Minutes";
                  }
                  foreach (var rsvp in activities.attendeesList)
                  {
                    if (rsvp.userID == HttpContext.Session.GetInt32("userID"))
                    {
                      RSVPed = true;
                    }
                    ++attendees;
                  }
                  Dictionary<string, object> newActivity = new Dictionary<string, object>();
                  newActivity.Add("activityID", activities.activityID);
                  newActivity.Add("title", activities.title);
                  newActivity.Add("sessionDateTime", activities.sessionDateTime);
                  newActivity.Add("duration", activities.duration);
                  newActivity.Add("dA", dA);
                  newActivity.Add("owner", owner);
                  newActivity.Add("owned", owned);
                  newActivity.Add("attendees", attendees);
                  newActivity.Add("RSVPed", RSVPed);
                  ActivityList.Add(newActivity);
                }
                ViewBag.activities = ActivityList;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "User");
            }
        }
// delete game session
        [HttpGet]
        [Route("delete/{ActivityId}")]
        public IActionResult Delete(int ActivityId)
        {
            Activity deleteTarget = _context.activities.SingleOrDefault(
            gs => gs.userID == (int)HttpContext.Session.GetInt32("userID") &&
            gs.activityID == ActivityId);
          if (deleteTarget != null)
          {
            _context.activities.Remove(deleteTarget);
            _context.SaveChanges();
          }
          return RedirectToAction("Splash");
        }
// un-planned attend session
        [HttpGet]
        [Route("UnRSVP/{ActivityId}")]
        public IActionResult UnRSVP(int ActivityId)
        {
          AttendeesList bailing = _context.attendeesList.SingleOrDefault(
            ps => ps.userID == (int)HttpContext.Session.GetInt32("userID") &&
            ps.activityID == ActivityId);
          if (bailing != null)
          {
            _context.attendeesList.Remove(bailing);
            _context.SaveChanges();
          }
          return RedirectToAction("Splash");
        }
// attend planned session
        [HttpGet]
        [Route("Respond/{ActivityID}")]
        public IActionResult Respond(int ActivityID)
        {
          AttendeesList newSession = new AttendeesList{
            userID = (int)HttpContext.Session.GetInt32("userID"),
            activityID = ActivityID
          };
          AttendeesList existingSession = _context.attendeesList.SingleOrDefault(
            r => r.userID == (int)HttpContext.Session.GetInt32("userID") &&
            r.activityID == ActivityID);
          if (existingSession == null)
          {
            _context.attendeesList.Add(newSession);
            _context.SaveChanges();
          }
          return RedirectToAction("Splash");
        }
// Add session Form
        [HttpGet]
        [Route("AddActivity")]
        public IActionResult AddActivity()
        {
          return View("AddActivity");
        }
//Post Game Form
        [HttpPost]
        [Route("create")]
        public IActionResult Create(AddActivityViewModel model)
        {
          if (ModelState.IsValid)
          {
            Activity newSession = new Activity{
              title = model.title,
              sessionDateTime = model.sessionDateTime,
              duration = model.duration,
              durationAnnotation = model.durationAnnotation,
              description = model.description,
              created_at = DateTime.UtcNow,
              updated_at = DateTime.UtcNow,
              userID = (int)HttpContext.Session.GetInt32("userID")
            };
            _context.activities.Add(newSession);
            _context.SaveChanges();
            return RedirectToAction("Splash");
          }
          else
          {
              Console.WriteLine("THIS IS THE ERORR");
            return View("AddActivity", model);
          }
        }

//Display individual Activitys
        [HttpGet]
        [Route("Activity/{ActivityId}")]
        public IActionResult Display(int ActivityId)
        {

            
          ViewBag.thisActivity =  _context.activities.Where(a => a.activityID == ActivityId)
            .Include(gs => gs.attendeesList)
            .ThenInclude(u => u.user).SingleOrDefault();
            // retur View("DisplayActivity");
            return View("DisplayActivity");
        }
    }
}
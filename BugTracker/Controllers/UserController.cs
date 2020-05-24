using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Data;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Controllers
{
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        // Dependency injection
        // Inject database contents into the controller
        private readonly BugTrackerContext _context;


        public UserController(BugTrackerContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }
    }
}
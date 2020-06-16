using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
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


        public async Task<IActionResult> Index()
        {
            var myUser = await _context.UserModels.FindAsync(User.Identity.GetUserId());

            return View(myUser);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BugTracker.Models;
using System.Runtime.InteropServices.WindowsRuntime;
using BugTracker.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BugTracker.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        // Dependency injection
        // Inject database contents into the controller
        private readonly BugTrackerContext _context;

        public HomeController(ILogger<HomeController> logger, BugTrackerContext context)
        {
            _logger = logger;
            _context = context;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            ClaimsPrincipal currentUser = this.User;
            if (currentUser.FindFirst(ClaimTypes.NameIdentifier) != null)
            {
                var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
                ViewBag.userId = currentUserID;
            }
            else
                ViewBag.userId = 0;


            // Pass Tickets through ViewBag
            // TODO: Convert to TicketList
            ViewBag.Tickets = await _context.Tickets.ToListAsync();


            return View(await _context.Projects.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
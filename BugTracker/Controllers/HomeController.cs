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
                ViewBag.userId = "0";

            LanguageImages c1 = new LanguageImages();
            ViewBag.projectImage = c1.getJava();

            // Pass Tickets through ViewBag
            // TODO: Convert to TicketList or something better....
            ViewBag.Tickets = await _context.Tickets.ToListAsync();
            List<Tickets> recentTickets = new List<Tickets>();

            recentTickets = getRecentTickets(ViewBag.Tickets);

            int i = 0;
            foreach (var item in recentTickets)
            {
                var Project = await _context.Projects.FindAsync(item.ProjectId);

                if (Project != null)
                {
                    // Do nothing?

                    // TEMPORARY SOLUTION. CONVERT LATER
                    switch (Project.projectLanguage)
                    {
                        case "C":
                            if (i == 0) { ViewBag.ticket1Image = c1.getC(); }
                            else if (i == 1) { ViewBag.ticket2Image = c1.getC(); }
                            else if (i == 2) { ViewBag.ticket3Image = c1.getC(); }
                            break;
                        case "CSharp":
                            if (i == 0) { ViewBag.ticket1Image = c1.getCsharp(); }
                            else if (i == 1) { ViewBag.ticket2Image = c1.getCsharp(); }
                            else if (i == 2) { ViewBag.ticket3Image = c1.getCsharp(); }
                            break;
                        case "Go":
                            if (i == 0) { ViewBag.ticket1Image = c1.getGo(); }
                            else if (i == 1) { ViewBag.ticket2Image = c1.getGo(); }
                            else if (i == 2) { ViewBag.ticket3Image = c1.getGo(); }
                            break;
                        case "Java":
                            if (i == 0) { ViewBag.ticket1Image = c1.getJava(); }
                            else if (i == 1) { ViewBag.ticket2Image = c1.getJava(); }
                            else if (i == 2) { ViewBag.ticket3Image = c1.getJava(); }
                            break;
                        case "JavaScipt":
                            if (i == 0) { ViewBag.ticket1Image = c1.getJavaScript(); }
                            else if (i == 1) { ViewBag.ticket2Image = c1.getJavaScript(); }
                            else if (i == 2) { ViewBag.ticket3Image = c1.getJavaScript(); }
                            break;
                        case "PHP":
                            if (i == 0) { ViewBag.ticket1Image = c1.getPHP(); }
                            else if (i == 1) { ViewBag.ticket2Image = c1.getPHP(); }
                            else if (i == 2) { ViewBag.ticket3Image = c1.getPHP(); }
                            break;
                        case "Python":
                            if (i == 0) { ViewBag.ticket1Image = c1.getPython(); }
                            else if (i == 1) { ViewBag.ticket2Image = c1.getPython(); }
                            else if (i == 2) { ViewBag.ticket3Image = c1.getPython(); }
                            break;
                        case "Ruby":
                            if (i == 0) { ViewBag.ticket1Image = c1.getRuby(); }
                            else if (i == 1) { ViewBag.ticket2Image = c1.getRuby(); }
                            else if (i == 2) { ViewBag.ticket3Image = c1.getRuby(); }
                            break;
                        case "SQL":
                            if (i == 0) { ViewBag.ticket1Image = c1.getSQL(); }
                            else if (i == 1) { ViewBag.ticket2Image = c1.getSQL(); }
                            else if (i == 2) { ViewBag.ticket3Image = c1.getSQL(); }
                            break;
                        case "Swift":
                            if (i == 0) { ViewBag.ticket1Image = c1.getSwift(); }
                            else if (i == 1) { ViewBag.ticket2Image = c1.getSwift(); }
                            else if (i == 2) { ViewBag.ticket3Image = c1.getSwift(); }
                            break;
                        case "TypeScript":
                            if (i == 0) { ViewBag.ticket1Image = c1.getTypeScript(); }
                            else if (i == 1) { ViewBag.ticket2Image = c1.getTypeScript(); }
                            else if (i == 2) { ViewBag.ticket3Image = c1.getTypeScript(); }
                            break;
                        default:
                            break;
                    }
                }

                if (i == 0)
                {
                    if (item != null)
                    {
                        ViewBag.ticket1 = item;
                        ViewBag.ticket1Color = getTicketColor(item);
                    }
                    else
                    {
                        ViewBag.ticket3 = new Tickets();
                    }
                }
                else if (i == 1)
                {
                    if (item != null)
                    {
                        ViewBag.ticket2 = item;
                        ViewBag.ticket2Color = getTicketColor(item);
                    }
                    else
                    {
                        ViewBag.ticket3 = new Tickets();
                    }
                }
                else if (i == 2)
                {
                    if (item != null)
                    {
                        ViewBag.ticket3 = item;
                        ViewBag.ticket3Color = getTicketColor(item);
                    }
                    else
                    {
                        ViewBag.ticket3 = new Tickets();
                    }
                }
                i++;
            }


            List<Projects> fullProjectList = await _context.Projects.ToListAsync();
            IEnumerable<Projects> userProjects = fullProjectList.Where(x => x.userId == ViewBag.userId);

            return View(userProjects);
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



        // Used in the index page to display the three most recent tickets
        public List<Tickets> getRecentTickets(List<Tickets> ticketList)
        {
            List<Tickets> recentTickets = new List<Tickets>();

            var ticket1 = new Tickets();
            var ticket2 = new Tickets();
            var ticket3 = new Tickets();

            int index = 0;

            index = 0;
            // Store the 3 most recent tickets
            foreach (var ticket in ticketList)
            {
                if (ticket.userId == ViewBag.userId)
                {

                    if (index % 3 == 0)
                    {
                        ticket1 = ticket;
                    }
                    else if (index % 3 == 1)
                    {
                        ticket2 = ticket;
                    }
                    else if (index % 3 == 2)
                    {
                        ticket3 = ticket;
                    }

                    index++;
                }
            }

            // Reorganization
            if (index % 3 == 0)
            {
                var tempT3 = ticket3;

                ticket3 = ticket1;
                ticket1 = tempT3;
            }
            else if (index % 3 == 1)
            {
                var tempT2 = ticket2;

                ticket2 = ticket3;
                ticket3 = tempT2;

            }
            else if (index % 3 == 2)
            {
                var tempT1 = ticket1;

                ticket1 = ticket2;
                ticket2 = tempT1;
            }

            recentTickets.Add(ticket1);
            recentTickets.Add(ticket2);
            recentTickets.Add(ticket3);

            return recentTickets;
        }

        /* COLORS
   "#d4edda"
   "#fff3cd"
   "#f8d7da"
   "#cce5ff"
*/

        public String getTicketColor(Tickets Ticket)
        {
            // Set color based on the ticket's priority
            String alertColor;
            switch ((int)Ticket.TicketPriority)
            {
                case 0:
                    alertColor = "#d4edda";
                    break;
                case 1:
                    alertColor = "#fff3cd";
                    break;
                case 2:
                    alertColor = "#f8d7da";
                    break;
                case 3:
                    alertColor = "#cce5ff";
                    break;
                default:
                    alertColor = "alert-secondary";
                    break;
            }

            return alertColor;
        }
    }
}
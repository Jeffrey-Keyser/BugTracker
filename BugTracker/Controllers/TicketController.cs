using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Data;
using System.Security.Claims;
using BugTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace BugTracker.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Route("[controller]/[action]")]
    public class TicketController : Controller
    {
        // Dependency injection
        // Inject database contents into the controller
        private readonly BugTrackerContext _context;


        public TicketController(BugTrackerContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.userId = currentUserID;

            return View(await _context.Tickets.ToListAsync());
        }

        public async Task<IActionResult> Ticket(string ? id)
        {
            // Find the correct ticket
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FirstOrDefaultAsync(m => m.TicketId == id);

            if (ticket == null)
            {
                return NotFound();
            }


            // Set color based on the ticket's priority
            String alertColor;
            switch ((int)ticket.TicketPriority)
            {
                case 0:
                    alertColor = "alert-success";
                    break;
                case 1:
                    alertColor = "alert-warning";
                    break;
                case 2:
                    alertColor = "alert-danger";
                    break;
                case 3:
                    alertColor = "alert-primary";
                    break;
                default:
                    alertColor = "alert-secondary";
                    break;
            }

            ViewBag.alertColor = alertColor;

            // Display card with project associated with the ticket
            var project = await _context.Projects.FindAsync(ticket.ProjectId);

            if (project == null)
            {
                return NotFound();
            }

            ViewBag.projectName = project.ProjectName;

            LanguageImages c1 = new LanguageImages();

            // TEMPORARY SOLUTION. CONVERT LATER
            switch (project.projectLanguage)
            {
                case "C":
                    ViewBag.ProjectImage = c1.getC();
                    break;
                case "CSharp":
                    ViewBag.ProjectImage = c1.getCsharp();
                    break;
                case "Go":
                    ViewBag.ProjectImage = c1.getGo();
                    break;
                case "Java":
                    ViewBag.ProjectImage = c1.getJava();
                    break;
                case "JavaScipt":
                    ViewBag.ProjectImage = c1.getJavaScript();
                    break;
                case "PHP":
                    ViewBag.ProjectImage = c1.getPHP();
                    break;
                case "Python":
                    ViewBag.ProjectImage = c1.getPython();
                    break;
                case "Ruby":
                    ViewBag.ProjectImage = c1.getRuby();
                    break;
                case "SQL":
                    ViewBag.ProjectImage = c1.getSQL();
                    break;
                case "Swift":
                    ViewBag.ProjectImage = c1.getSwift();
                    break;
                case "TypeScript":
                    ViewBag.ProjectImage = c1.getTypeScript();
                    break;
                default:
                    break;
            }


            return View(ticket);
        }

        public async Task<IActionResult> Project(string? id)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.userId = currentUserID;


            // Pass in the project we wish to associate with the Ticket
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FirstOrDefaultAsync(m => m.projectId == id);
            if (project == null)
            {
                return NotFound();
            }

            ViewBag.ProjectId = project.projectId;


            return View(await _context.Tickets.ToListAsync());
        }

        public async Task<IActionResult> Create(string? id)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.userId = currentUserID;
            ViewBag.projectId = id;
            ViewBag.creationDate = DateTime.Now;

            // For Priority?
            //   ViewBag.PriorityList = new SelectList(Enum.GetValues(typeof(Priority)), "TicketPriority", "Description", 0);


            // @Html.DropDownList("TicketPriority",
            //             new SelectList(Enum.GetValues(typeof(Priority))),
            //            "Select Priority",
            //            new { @class = "form-control" })


            // Pass in the project we wish to associate with the Ticket
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FirstOrDefaultAsync(m => m.projectId == id);
            ViewBag.projectName = project.ProjectName;
            ViewBag.authorName = project.Author;

            if (project == null)
            {
                return NotFound();
            }

            ViewBag.Project = project;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string? id, [Bind("TicketId, TicketName, TicketDesc, userId, ProjectId, TicketPriority, projectName, authorName")] Tickets Ticket)
        {
            // 0:dd/MMM/yyyy HH:mm:ss
            // Get Current DateTime
            ViewBag.creationDate = DateTime.Now; //.ToString("0:dd/MMM/yyyy HH:mm:ss");


            ViewBag.userId = Ticket.userId;
            ViewBag.projectId = Ticket.ProjectId;

            Ticket.CreationDate = DateTime.Now; //DateTime.Now.ToString("0:dd/MMM/yyyy HH:mm:ss");
            // Add the ticket to the project's 

            if (Ticket.ProjectId == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FirstOrDefaultAsync(m => m.projectId == Ticket.ProjectId);
            if (project != null)
            {
                project.TicketList.Add(Ticket);
            }

            if (project == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Tickets.Add(Ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction("Project", new { id = Ticket.ProjectId });
            }

            return View(Ticket);
        }

        /*
        public async Task<IActionResult> Complete(int ? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FirstOrDefaultAsync(m => m.TicketId == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }
        */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Complete(string id)
        {
            var Ticket = await _context.Tickets.FindAsync(id);
            if (Ticket == null)
            {
                return NotFound();
            }

            if (!Ticket.Completed)
            {
                Ticket.Completed = true;
            }
            else
            {
                Ticket.Completed = false;
            }

            _context.Tickets.Update(Ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction("Project", new { id = Ticket.ProjectId });
        }

        /*
        public async Task<IActionResult> Edit(string id)
        {

            // Pass in the project we wish to associate with the Ticket
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticket == null)
            {
                return NotFound();
            }

            ViewBag.Ticket = ticket;

            return View();
        } */


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TicketId, TicketName, TicketDesc, userId, ProjectId, TicketPriority")] Tickets Ticket)
        {

        /*    if (id != Ticket.TicketId)
            {
                return NotFound();
            } */

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction("Project" , new { id = Ticket.ProjectId } );
            }
            return View(Ticket);
        }


        [HttpPost, ActionName("Delete")]
        //      [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {

            var Ticket = await _context.Tickets.FindAsync(id);
            if (Ticket == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(Ticket);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}
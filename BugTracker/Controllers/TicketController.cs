﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Data;
using System.Security.Claims;
using BugTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Controllers
{
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

            return View(await _context.Projects.ToListAsync());
        }

        public async Task<IActionResult> Ticket(int ? id)
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

            return View(ticket);
        }

        public async Task<IActionResult> Project(int ? id)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.userId = currentUserID;


            // Pass in the project we wish to associate with the Ticket
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            ViewBag.ProjectId = project.Id;


            return View(await _context.Tickets.ToListAsync());
        }

        public async Task<IActionResult> Create(int ? id)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.userId = currentUserID;
            ViewBag.projectId = id;

            // Pass in the project we wish to associate with the Ticket
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.FirstOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            ViewBag.Project = project;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int ? id, [Bind("TicketId, TicketName, TicketDesc, userId, ProjectId")] Tickets Ticket)
        {
           // ClaimsPrincipal currentUser = this.User;
          //  var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.userId = Ticket.userId;
            ViewBag.projectId = Ticket.ProjectId;


            // Add the ticket to the project's TicketList
            if (Ticket.ProjectId == 0)
            {
                return NotFound();
            }

            var project = await _context.Projects.FirstOrDefaultAsync(m => m.Id == Ticket.ProjectId);
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
                _context.Add(Ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction("Project", new { id = Ticket.ProjectId });
            }

            return View(Ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Complete(int id)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(m => m.TicketId == id);

            if (ticket == null)
            {
                return NotFound();
            }

            if (!ticket.Completed)
            {
                ticket.Completed = true;
            }
            else
            {
                ticket.Completed = false;
            }

            _context.Update(ticket);
            await _context.SaveChangesAsync();
            return View(ticket);
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

    }
}
﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using BugTracker.Data;
using BugTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.View;
using Newtonsoft.Json;
using static BugTracker.Models.TicketChart;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BugTracker.Controllers
{
    [Route("[controller]/[action]")]
    public class ProjectController : Controller
    {

        // Dependency injection
        // Inject database contents into the controller
        private readonly BugTrackerContext _context;

        public ProjectController(BugTrackerContext context)
        {
            _context = context;
        }

        //public Projects Project { get; set; }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.userId = currentUserID;

            return View(await _context.Projects.ToListAsync());
        }

        // First Lookup on Edit
        public async Task<IActionResult> Edit(int? id)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.userId = currentUserID;

            if (id == null)
            {
                return NotFound();
            }

            var Project = await _context.Projects.FindAsync(id);
            if (Project == null)
            {
                return NotFound();
            }
            return View(Project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Author, ProjectName, userId, projectLanguage")] Projects Project)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.userId = currentUserID;


            if (id != Project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(Project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction("Index");
            }
            return View(Project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.userId = currentUserID;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, Author, ProjectName, userId, projectLanguage")] Projects Project)
        {
            ClaimsPrincipal currentUser = this.User;
            var currentUserID = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            ViewBag.userId = currentUserID;

            var currUser = await _context.UserModels.FirstOrDefaultAsync(m => m.key == currentUserID);

            currUser.AuthoredProjects.Add(Project);

            if (ModelState.IsValid)
            {

                _context.Add(Project);



                await _context.SaveChangesAsync();


                return RedirectToAction("Index");
            }

            return View(Project);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Delete all associated tickets
            // Do Before Project
            await deleteTickets(id);

            var Project = await _context.Projects.FindAsync(id);
            if (Project == null)
            {
                return NotFound();
            }

            _context.Projects.Remove(Project);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Project(int id)
        {


            List<DataPoint> barDataPoints = new List<DataPoint>();
            List<DataPoint> lineDataPoints = new List<DataPoint>();

            var Project = await _context.Projects.FindAsync(id);
            if (Project == null)
            {
                return NotFound();
            }

            var ticketList = await _context.Tickets.ToListAsync();

            int num_low = 0, num_med = 0, num_high = 0, num_crit = 0;

            // Add all tickets that match the project to the graph
            // For the bar graph
            foreach (var item in Project.TicketList)
            {
                if (item.ProjectId == Project.Id && !item.Completed)
                {
                    switch (Enum.GetName(typeof(Priority), item.TicketPriority))
                    {
                        case "Low":
                            num_low++;
                            break;
                        case "Medium":
                            num_med++;
                            break;
                        case "High":
                            num_high++;
                            break;
                        case "Critical":
                            num_crit++;
                            break;
                    }
                }
            }

            // Return color for the highest priority
            ICollection<Color> colors = getProjectCornerColors(num_low, num_med, num_high, num_crit);
            if (colors.Count() == 1)
            {
                IEnumerator en = colors.GetEnumerator();
                en.MoveNext();
                ViewBag.projectCornerColorPrimary = ColorTranslator.ToHtml((Color)en.Current);
                ViewBag.projectCornerColorSecond = "#A7ADB7";
            }


            barDataPoints.Add(new DataPoint("Low", num_low));
            barDataPoints.Add(new DataPoint("Medium", num_med));
            barDataPoints.Add(new DataPoint("High", num_high));
            barDataPoints.Add(new DataPoint("Critical", num_crit));


            ViewBag.BarDataPoints = JsonConvert.SerializeObject(barDataPoints);


            DateTime currTime = DateTime.Now.Date.AddDays(1);

            int today = 0, oneDayBack = 0, twoDaysBack = 0, threeDaysBack = 0, fourDaysBack = 0,
            fiveDaysBack = 0, sixDaysBack = 0, sevenDaysBack = 0;

            // For the Line Graph
            foreach (var item in Project.TicketList)
            {
                if (item.ProjectId == Project.Id)
                {
                    switch ((currTime - item.CreationDate).Days)
                    {
                        case 0: today++; break; // today
                        case 1: oneDayBack++; break;
                        case 2: twoDaysBack++; break;
                        case 3: threeDaysBack++; break;
                        case 4: fourDaysBack++; break;
                        case 5: fiveDaysBack++; break;
                        case 6: sixDaysBack++; break;
                        case 7: sevenDaysBack++; break; // a week ago
                        default: break;
                    }
                }
            }

            DayOfWeek day = DateTime.Now.DayOfWeek;

            lineDataPoints.Add(new DataPoint(Enum.GetName(typeof(DayOfWeek), day), sevenDaysBack));

            if (((int)day) == 6)
                day = 0;
            else
                day++;

            lineDataPoints.Add(new DataPoint(Enum.GetName(typeof(DayOfWeek), day), sixDaysBack));


            if (((int)day) == 6)
                day = 0;
            else
                day++;

            lineDataPoints.Add(new DataPoint(Enum.GetName(typeof(DayOfWeek), day), fiveDaysBack));

            if (((int)day) == 6)
                day = 0;
            else
                day++;

            lineDataPoints.Add(new DataPoint(Enum.GetName(typeof(DayOfWeek), day), fourDaysBack));

            if (((int)day) == 6)
                day = 0;
            else
                day++;

            lineDataPoints.Add(new DataPoint(Enum.GetName(typeof(DayOfWeek), day), threeDaysBack));


            if (((int)day) == 6)
                day = 0;
            else
                day++;

            lineDataPoints.Add(new DataPoint(Enum.GetName(typeof(DayOfWeek), day), twoDaysBack));


            if (((int)day) == 6)
                day = 0;
            else
                day++;

            lineDataPoints.Add(new DataPoint(Enum.GetName(typeof(DayOfWeek), day), oneDayBack));


            if (((int)day) == 6)
                day = 0;
            else
                day++;

            lineDataPoints.Add(new DataPoint("Today", today));

            ViewBag.LineDataPoints = JsonConvert.SerializeObject(lineDataPoints);

            return View(Project);
        }

        [HttpDelete] // Was POST
        // BUG: Cannot Handle multiple tickets without error
        // Delete all tickets associated with a project
        public async Task deleteTickets(int projectId)
        {
            var ticketList = await _context.Tickets.ToListAsync();

            foreach (var item in ticketList)
            {
                if (item.ProjectId == projectId)
                {
                    _context.Tickets.Remove(item);
                    await _context.SaveChangesAsync();
                }
            }

            return;

        }


         /* COLORS
            "#d4edda"
            "#fff3cd"
            "#f8d7da"
            "#cce5ff"
         */
        public ICollection<Color> getProjectCornerColors(int num_low, int num_med, int num_high, int num_crit)
        {
            ICollection<Color> chosenColors = new Collection<Color>();

            Color low = ColorTranslator.FromHtml("#d4edda");
            Color med = ColorTranslator.FromHtml("#fff3cd");
            Color high = ColorTranslator.FromHtml("#f8d7da");
            Color crit = ColorTranslator.FromHtml("#cce5ff");
            Color defaultcolor = ColorTranslator.FromHtml("#A7ADB7");

            // Strictly higher, just choose one color
            if (num_low > num_med & num_low > num_high & num_low > num_crit)
            {
                chosenColors.Add(low);
            }
            else if (num_med > num_low & num_med > num_high & num_med > num_crit)
            {
                chosenColors.Add(med);
            }
            else if (num_high > num_low & num_high > num_med & num_high > num_crit)
            {
                chosenColors.Add(high);
            }
            else if(num_crit > num_low & num_crit > num_med & num_crit > num_high)
            {
                chosenColors.Add(crit);
            }
            else
            {
                chosenColors.Add(defaultcolor);
            }

            return chosenColors;
        }
    }
}
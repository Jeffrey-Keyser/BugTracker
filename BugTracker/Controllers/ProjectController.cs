using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BugTracker.Data;
using BugTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

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
            return View(await _context.Projects.ToListAsync());
        }

        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return null;
            }

            Projects Project = _context.Projects.Find(id);
            if (Project == null)
            {
                return null;
            }
            return View(Project);
        }

        // GET: Projects/Create
        public IActionResult Create(Projects Project)
        {
            return View(Project);
        }

   /*     [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind ("Id, Author, ProjectName")] Projects Project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(Project);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(Project);
        }
        */
    }
}

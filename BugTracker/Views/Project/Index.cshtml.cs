using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Data;
using BugTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Views.Project
{
    public class IndexModel : PageModel
    {
        private readonly BugTrackerContext _db;

        public IndexModel(BugTrackerContext db)
        {
            _db = db;
        }
       
        public IEnumerable<Projects> Projects { get; set; }
        public async Task OnGet()
        {
            Projects = await _db.Projects.ToListAsync();
        } 
    }
}

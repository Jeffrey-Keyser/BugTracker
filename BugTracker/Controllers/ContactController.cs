using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BugTracker.Controllers
{
    public class ContactController : Controller
    {
        [Route("[controller]/[action]")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
using BugTracker.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class UserProject
    {
        public string projectId { get; set; }

        public Projects Project { get; set; }

        public string Id { get; set; }

        public BugTrackerUser BugTrackerUser { get; set; }
    }
}

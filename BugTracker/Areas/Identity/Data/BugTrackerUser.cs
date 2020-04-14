using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BugTracker.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the BugTrackerUser class
    public class BugTrackerUser : IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; }
    }
}

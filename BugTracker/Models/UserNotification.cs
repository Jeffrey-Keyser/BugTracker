using BugTracker.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class UserNotification
    {

        public string AuthorId { get; set; }
        public BugTrackerUser NotificationAuthor { get; set; }

        public string AffectedId { get; set; }


        // Loop through users when creating notification
        public BugTrackerUser AffectedUser { get; set; }

        public string NotificationMessage { get; set; }

        public Boolean NotificationSeen { get; set; }

    }
}

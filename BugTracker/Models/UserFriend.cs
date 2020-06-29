using BugTracker.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTracker.Models
{
    public class UserFriend
    {

        public string ? senderId { get; set; }
        public BugTrackerUser sender { get; set; }


        public string ? recieverId { get; set; }
        public BugTrackerUser reciever { get; set; }


        public Boolean sent { get; set; } = false;


        public Boolean accepted { get; set; } = false;

    }
}

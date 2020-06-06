using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BugTracker.Models;
//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;

namespace BugTracker.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the BugTrackerUser class
    public class BugTrackerUser : IdentityUser
    {
    /*    public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<BugTrackerUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateAsync(this);
            // Add custom user claims here
            return userIdentity;
        } */

        [PersonalData]
        [DisplayName("Full Name")]
        public string Name { get; set; }

        [PersonalData]
        [InverseProperty("UserCreatedProject")]
        public ICollection<Projects> AuthoredProjects { get; set; } = new Collection<Projects>();


        // Projects that the user has been invited to collaborate on
        [PersonalData]
        [InverseProperty("UserAssignedProject")]
        public ICollection<Projects> AssignedProjects { get; set; } = new Collection<Projects>();

        [PersonalData]
        [Required]
        public int numCompletedTickets { get; set; } = 0;

        [PersonalData]
        [Required]
        public int numCompletedProjects { get; set; } = 0;


        // Completed Tickets
        [PersonalData]
        public ICollection<Tickets> CompletedTickets { get; set; } = new Collection<Tickets>();

        [PersonalData]
        [Required] // DataType(DataType.DateTime)
        [DisplayFormat(DataFormatString = "{0:dd/MMM/yyyy HH:mm:ss}")]
        public DateTime CreationDate { get; set; }


        public static implicit operator BugTrackerUser(string v)
        {
            throw new NotImplementedException();
        }
    }
}

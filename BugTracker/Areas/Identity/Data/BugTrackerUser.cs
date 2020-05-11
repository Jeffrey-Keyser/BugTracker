﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;

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

        public string FirstName { get; set; }

        public static implicit operator BugTrackerUser(string v)
        {
            throw new NotImplementedException();
        }
    }
}
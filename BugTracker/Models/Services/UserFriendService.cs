using BugTracker.Areas.Identity.Data;
using BugTracker.Data;
using IdentityModel;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BugTracker.Models.Services
{
    public class UserFriendService
    {
        private readonly BugTrackerContext _context;
        private readonly UserManager<BugTrackerUser> _userManager;
        private readonly SignInManager<BugTrackerUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserFriendService( BugTrackerContext context,
                                    UserManager<BugTrackerUser> userManager,
                                    SignInManager<BugTrackerUser> signInManager,
                                    IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public int numFriendRequests()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            int requests;

            var result =  _context.UserFriends.ToList()
                            .Where(r => r.recieverId == userId)
                            .Where(p => p.accepted == false)
                            .ToList();

            requests = result.Count();

            return requests;
        }


        public ICollection<UserFriend> getFriendRequests()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            ICollection<UserFriend> result = _context.UserFriends
                .Where(r => r.recieverId == userId)
                .Where(p => p.accepted == false)
                .Include(p => p.sender)
                .ToList();

            return result;
        }

        public int getFriendCount()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            int friends;

            var result1 = _context.UserFriends.ToList()
                            .Where(r => r.recieverId == userId)
                            .Where(p => p.accepted == true)
                            .ToList();

            var result2 = _context.UserFriends.ToList()
                .Where(r => r.senderId == userId)
                .Where(p => p.accepted == true)
                .ToList();

            friends = result1.Count() + result2.Count();

            return friends;


        }

    }
}

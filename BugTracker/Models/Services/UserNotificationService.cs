using BugTracker.Areas.Identity.Data;
using BugTracker.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BugTracker.Models.Services
{
    public class UserNotificationService
    {


        private readonly BugTrackerContext _context;
        private readonly UserManager<BugTrackerUser> _userManager;
        private readonly SignInManager<BugTrackerUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserNotificationService(BugTrackerContext context,
                                    UserManager<BugTrackerUser> userManager,
                                    SignInManager<BugTrackerUser> signInManager,
                                    IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
        }


        public int numNotifications()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            int notifications;

            var result = _context.UserNotifications.ToList()
                            .Where(r => r.AffectedId == userId)
                            .Where(p => p.NotificationSeen == false)
                            .Where(b => b.Bug == false)
                            .ToList();

            notifications = result.Count();

            return notifications;
        }

        public int numBugs()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            int notifications;

            var result = _context.UserNotifications.ToList()
                            .Where(r => r.AffectedId == userId)
                            .Where(p => p.NotificationSeen == false)
                            .Where(b => b.Bug == true)
                            .ToList();

            notifications = result.Count();

            return notifications;
        }


        public ICollection<UserNotification> GetNotifications(bool bug)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            ICollection<UserNotification> result = _context.UserNotifications
                .Where(r => r.AffectedId == userId)
                .Where(p => p.NotificationSeen == false)
                .Where(b => b.Bug == bug)
                .Include(j => j.NotificationAuthor)
                .ToList();

            return result;
        }

    }
}

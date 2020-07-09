using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Data;
using BugTracker.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        // Dependency injection
        // Inject database contents into the controller
        private readonly BugTrackerContext _context;


        public UserController(BugTrackerContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var myUser = await _context.UserModels.FindAsync(User.Identity.GetUserId());

            return View(myUser);
        }


        public async Task<IActionResult> AddFriend(string recieverId, string senderId, bool accepted)
        {

            // Decision is whether to accept or deny request

            // Should just be one 
            UserFriend userFriend = await _context.UserFriends
                                        .Where(i => i.recieverId == recieverId)
                                        .Where(j => j.senderId == senderId)
                                        .Include(b => b.sender)
                                        .Include(c => c.reciever)
                                        .SingleOrDefaultAsync();

            if (accepted)
            {

                userFriend.accepted = true;

                _context.UserFriends.Update(userFriend);

            }
            else // Delete the request from the database
            {
                _context.UserFriends.Remove(userFriend);
            }


            var result = await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> SeenNotification(string authorId, string affectedId)
        {

            UserNotification notification = await _context.UserNotifications
                                                .Where(i => i.AuthorId == authorId)
                                                .Where(j => j.AffectedId == affectedId)
                                                .SingleOrDefaultAsync();

            // Hmm.... maybe dont need this variable
            notification.NotificationSeen = true;


            // Just remove the notification from database, save space
            _context.Remove(notification);

            var result = await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Home");


        }

    }
}
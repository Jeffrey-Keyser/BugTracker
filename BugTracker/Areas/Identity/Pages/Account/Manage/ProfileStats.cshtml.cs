using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Areas.Identity.Data;
using BugTracker.Data;
using BugTracker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Areas.Identity.Pages.Account.Manage
{
    public class ProfileStatsModel : PageModel
    {
        private readonly UserManager<BugTrackerUser> _userManager;
        private readonly SignInManager<BugTrackerUser> _signInManager;

        // Dependency injection
        // Inject database contents into the controller
        private readonly BugTrackerContext _context;

        public ProfileStatsModel(
            UserManager<BugTrackerUser> userManager,
            SignInManager<BugTrackerUser> signInManager,
            BugTrackerContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public string myUserId { get; set; }

        public virtual ICollection<UserProject> myUserProjects { get; set; } = new List<UserProject>();

        public ICollection<UserFriend> UserFriends { get; set; } = new System.Collections.ObjectModel.Collection<UserFriend>();

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [DataType(DataType.Text)]
            [Display(Name = "Enter Friend's Email")]
            public string FriendUserName { get; set; }
        }

        private async Task LoadAsync(BugTrackerUser user)
        {
            myUserId = user.Id;

            // Load the FriendsList
            UserFriends = await _context.UserFriends
                                .Where(b => b.accepted == true)
                                .Where(b => b.recieverId == user.Id)
                                .Include(b => b.sender)
                                .ToListAsync();

            Input = new InputModel()
            {
                FriendUserName = ""
            };

        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            BugTrackerUser user = await _userManager.GetUserAsync(User);

            bool requestSent = false;

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Input.FriendUserName != "")
            {
                foreach (var item in _context.Users)
                {
                    if (Input.FriendUserName == item.UserName)
                    {
                        // First search to make sure there isn't a request pending
                        var similarRequest1 = await _context.UserFriends
                                            .Where(i => i.senderId == user.Id)
                                            .Where(j => j.recieverId == item.Id)
                                            .ToListAsync();

                        // Try both sender and reciever switched

                        var similarRequest2 = await _context.UserFriends
                                            .Where(i => i.senderId == item.Id)
                                            .Where(j => j.recieverId == user.Id)
                                            .ToListAsync();

                        if (similarRequest1.Count() > 0 || similarRequest2.Count() > 0)
                        { } // Do nothing, request is already pending
                        else
                        {
                            // Create new friend request
                            requestSent = true;

                            UserFriend newFriendRequest = new UserFriend { sender = user, senderId = user.Id, reciever = item, recieverId = item.Id, accepted = false, sent = true };

                            _context.UserFriends.Add(newFriendRequest);
                        }
                        break;
                    }
                }
            }

            var result = await _context.SaveChangesAsync();

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);

            if (requestSent)
                TempData["Msg"] = "Success";
            else
                TempData["Msg"] = "Failure";

            return RedirectToPage();
        }


    }
}

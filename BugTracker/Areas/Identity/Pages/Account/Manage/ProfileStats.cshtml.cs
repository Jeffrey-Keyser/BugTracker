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

        public class InputModel
        {

        }

        private async Task LoadAsync(UserModel user)
        {
            var myUserId = user.key;

        }

        public async Task<IActionResult> OnGetAsync()
        {
            var identityUser = await _userManager.GetUserAsync(User);
            var user = await _context.UserModels.FindAsync(identityUser.Id);

            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

           

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);


            return RedirectToPage();
        }
    }
}

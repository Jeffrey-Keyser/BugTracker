using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BugTracker.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BugTracker.Areas.Identity.Pages.Account.Manage
{
    public class ProfileStatsModel : PageModel
    {
        private readonly UserManager<BugTrackerUser> _userManager;
        private readonly SignInManager<BugTrackerUser> _signInManager;

        public ProfileStatsModel(
            UserManager<BugTrackerUser> userManager,
            SignInManager<BugTrackerUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public class InputModel
        {

        }

        private async Task LoadAsync(BugTrackerUser user)
        {

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
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

           

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);


            return RedirectToPage();
        }
    }
}

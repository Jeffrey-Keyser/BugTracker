using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using BugTracker.Areas.Identity.Data;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace BugTracker.Extensions
{
	public class AdditionalUserClaimsPrincipalFactory
			: UserClaimsPrincipalFactory<BugTrackerUser, IdentityRole>
	{
		public AdditionalUserClaimsPrincipalFactory(
			UserManager<BugTrackerUser> userManager,
			RoleManager<IdentityRole> roleManager,
			IOptions<IdentityOptions> optionsAccessor)
			: base(userManager, roleManager, optionsAccessor)
		{ }

		public async override Task<ClaimsPrincipal> CreateAsync(BugTrackerUser user)
		{
			var principal = await base.CreateAsync(user);
			var identity = (ClaimsIdentity)principal.Identity;

			var claims = new List<Claim>();
			if (user.FirstName != null)
			{
				claims.Add(new Claim(JwtClaimTypes.Role, user.FirstName));
			}

			identity.AddClaims(claims);
			return principal;
		}
	}
}

using AcmeCorporation.Core.Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AcmeCorporation.Core.Common.Extensions
{
	public static class ClaimsPrincipalExtensions
	{
		public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
		{
			return claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		}
		/// <summary>
		/// Poor mans authentication used due to issue with setup of identity provider...
		/// </summary>
		/// <param name="claimsPrincipal"></param>
		/// <param name="userManager"></param>
		/// <returns></returns>
		public static async Task<bool> IsAdminAsync(this ClaimsPrincipal claimsPrincipal, UserManager<ApplicationUser> userManager)
		{
			var user = await userManager.FindByIdAsync(claimsPrincipal.GetUserId());
			if (user is null)
			{
				return false;
			}
			var claims = await userManager.GetClaimsAsync(user);
			return claims.Any(x => x.Type == "admin");
		}
	}
}

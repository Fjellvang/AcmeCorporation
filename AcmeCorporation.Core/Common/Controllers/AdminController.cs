using AcmeCorporation.Core.Common.Extensions;
using AcmeCorporation.Core.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeCorporation.Core.Common.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AdminController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> userManager;

		public AdminController(UserManager<ApplicationUser> userManager)
		{
			this.userManager = userManager;
		}

		[HttpGet(nameof(IsAdmin))]
		public async Task<IActionResult> IsAdmin()
		{
			return Ok(new { isAdmin = await this.User.IsAdminAsync(userManager) });
		}
	}
}

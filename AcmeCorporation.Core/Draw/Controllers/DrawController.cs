using AcmeCorporation.Core.Common.Dtos;
using AcmeCorporation.Core.Common.Extensions;
using AcmeCorporation.Core.Common.Utilities;
using AcmeCorporation.Core.Data.Models;
using AcmeCorporation.Core.Draw.Dtos;
using AcmeCorporation.Core.Draw.Extensions;
using AcmeCorporation.Core.Draw.Services;
using AcmeCorporation.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AcmeCorporation.Core.Draw
{
	[ApiController]
	[Route("api/[controller]")]
	public class DrawController : ControllerBase
	{
		private readonly AcmeCorporationDbContext context;
		private readonly ILogger<DrawController> logger;
		private readonly IDrawSubmissionService drawSubmissionService;
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly UserManager<ApplicationUser> userManager;

		public DrawController(AcmeCorporationDbContext context, ILogger<DrawController> logger, IDrawSubmissionService drawSubmissionService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
		{
			this.context = context;
			this.logger = logger;
			this.drawSubmissionService = drawSubmissionService;
			this.signInManager = signInManager;
			this.userManager = userManager;
		}


		[HttpGet(nameof(GetAllSubmissions)), Authorize(policy: "admin")]
		public async Task<IActionResult> GetAllSubmissions(int page = 0, int pageSize = 10, CancellationToken cancellationToken = default)
		{
			//Poor mans authentication....
			if (await this.User.IsAdminAsync(userManager))
			{
				var query = context.Serials.Where(x => x.UserRelation != null)
					.Select(x => new AdminSubmissionView(x.UserRelation.User.Email, x.Key, x.UserRelation.Uses))
					;
				var totalResults = query.Count();
				var submissions = await query
					.Skip(page * pageSize)
					.Take(pageSize)
					.ToArrayAsync(cancellationToken)
					;

				return Ok(new PaginatedResult<AdminSubmissionView>(submissions, totalResults));
			}
			return Ok(new PaginatedResult<AdminSubmissionView>(Array.Empty<AdminSubmissionView>(),0));
		}


		[HttpPost(nameof(SubmitDrawAuthorized)), Authorize]
		public async Task<IActionResult> SubmitDrawAuthorized([FromQuery] Guid serial)
		{
			if (serial == Guid.Empty)
			{
				return BadRequest(HttpUtilities.BadRequestProblemDetails("Error: Invalid serial provided"));
			}
			var result = await drawSubmissionService.SubmitSerialAsync(this.User.GetUserId(), serial);
			if (result == SubmissionResult.Success)
			{
				return Ok();
			}
			return BadRequest(HttpUtilities.BadRequestProblemDetails(result.GetErrorMessage()));
		}

		[HttpPost(nameof(SubmitDraw))]
		public async Task<IActionResult> SubmitDraw([FromBody] DrawSubmissionView submission)
		{
			if (!submission.AboveEighteen)
			{
				return BadRequest(HttpUtilities.BadRequestProblemDetails("User Needs to be above 18."));
			}
			if (submission.Serial == Guid.Empty)
			{
				return BadRequest(HttpUtilities.BadRequestProblemDetails("Error: Invalid serial provided"));
			}

			var result = await drawSubmissionService.SubmitSerialAsync(submission);

			if (result == SubmissionResult.Success)
			{
				var user = await userManager.FindByEmailAsync(submission.Email);
				await signInManager.SignInAsync(user, true);
				return Ok();
			}

			return BadRequest(HttpUtilities.BadRequestProblemDetails(result.GetErrorMessage()));
		}
	}
}

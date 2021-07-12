using AcmeCorporation.Core.Common.Utilities;
using AcmeCorporation.Core.Data.Models;
using AcmeCorporation.Core.Draw.Dtos;
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

		[HttpGet(nameof(Test))]
		public IActionResult Test()
		{
			return Ok("HEJ");
		}

		[HttpGet(nameof(GetAllSubmissions)), Authorize(policy: "admin")]
		public async Task<IActionResult> GetAllSubmissions(int page = 0, int pageSize = 10, CancellationToken cancellationToken = default)
		{
			//Poor mans authentication....
			var user = await userManager.FindByIdAsync(this.User.FindFirst(ClaimTypes.NameIdentifier).Value);
			var claims = await userManager.GetClaimsAsync(user);
			if (claims.Any(x => x.Type == "admin"))
			{
				var submissions = await context.Serials.Where(x => x.UserRelation != null)
					.Select(x => new { x.UserRelation.User.Email, Serial = x.Key })
					.Skip(page*pageSize)
					.Take(pageSize)
					.ToArrayAsync(cancellationToken)
					;

				return Ok(submissions);
			}
			return Ok(Array.Empty<object>());
		}

		[HttpPost(nameof(SubmitDrawAuthorized)), Authorize]
		public async Task<IActionResult> SubmitDrawAuthorized(Guid serial)
		{
			var user = await userManager.GetUserAsync(this.User);

			var result = await drawSubmissionService.SubmitSerialAsync(user.Id, serial);
			if (result == SubmissionResult.Success)
			{
				return Ok();
			}
			//TODO: Provide better response.
			return BadRequest(HttpUtilities.BadRequestProblemDetails("There was an error in submitting the serial."));
		}

		[HttpPost(nameof(SubmitDraw))]
		public async Task<IActionResult> SubmitDraw([FromBody] DrawSubmissionView submission)
		{
			if (!submission.AboveEighteen)
			{
				return BadRequest(HttpUtilities.BadRequestProblemDetails("User Needs to be above 18."));
			}

			var result = await drawSubmissionService.SubmitSerialAsync(submission);

			if (result == SubmissionResult.Success)
			{
				var user = await userManager.FindByEmailAsync(submission.Email);
				await signInManager.SignInAsync(user, true);
			}

			return Ok();
		}

		private static ProblemDetails BadRequestProblemDetails(string title, string message = null)
		{
			return new ProblemDetails() { Status = StatusCodes.Status400BadRequest, Title = title, Detail = message};
		}
	}
}

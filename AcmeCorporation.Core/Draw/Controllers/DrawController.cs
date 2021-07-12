using AcmeCorporation.Core.Data.Models;
using AcmeCorporation.Core.Draw.Dtos;
using AcmeCorporation.Core.Draw.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeCorporation.Core.Draw
{
	[ApiController]
	[Route("api/[controller]")]
	public class DrawController : ControllerBase
	{
		private readonly ILogger<DrawController> logger;
		private readonly IDrawSubmissionService drawSubmissionService;
		private readonly SignInManager<ApplicationUser> signInManager;
		private readonly UserManager<ApplicationUser> userManager;

		public DrawController(ILogger<DrawController> logger, IDrawSubmissionService drawSubmissionService, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
		{
			this.logger = logger;
			this.drawSubmissionService = drawSubmissionService;
			this.signInManager = signInManager;
			this.userManager = userManager;
		}

		[HttpPost(nameof(SubmitDraw))]
		public async Task<ActionResult> SubmitDraw([FromBody] DrawSubmissionView submission)
		{
			if (!submission.AboveEighteen)
			{
				return base.BadRequest(BadRequestProblemDetails("User Needs to be above 18."));
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

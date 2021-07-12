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

		public DrawController(ILogger<DrawController> logger, IDrawSubmissionService drawSubmissionService, SignInManager<ApplicationUser> signInManager)
		{
			this.logger = logger;
			this.drawSubmissionService = drawSubmissionService;
			this.signInManager = signInManager;
		}

		[HttpPost(nameof(SubmitDraw))]
		public ActionResult SubmitDraw([FromBody] DrawSubmissionView submission)
		{
			if (!submission.AboveEightteen)
			{
				return base.BadRequest(BadRequestProblemDetails("User Needs to be above 18."));
			}


			return Ok();
		}

		private static ProblemDetails BadRequestProblemDetails(string title, string message = null)
		{
			return new ProblemDetails() { Status = StatusCodes.Status400BadRequest, Title = title, Detail = message};
		}
	}
}

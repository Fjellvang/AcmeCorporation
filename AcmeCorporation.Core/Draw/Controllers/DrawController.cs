using AcmeCorporation.Core.Draw.Dtos;
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

		public DrawController(ILogger<DrawController> logger)
		{
			this.logger = logger;
		}

		[HttpPost(nameof(SubmitDraw))]
		public ActionResult SubmitDraw([FromBody] DrawSubmissionView submission)
		{
			return Ok();
		}
	}
}

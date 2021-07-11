using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeCorporation.Core.Draw
{
	public class DrawController : Controller
	{
		private readonly ILogger<DrawController> logger;

		public DrawController(ILogger<DrawController> logger)
		{
			this.logger = logger;
		}
	}
}

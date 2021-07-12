using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeCorporation.Core.Common.Utilities
{
	public static class HttpUtilities
	{
		public static ProblemDetails BadRequestProblemDetails(string title, string message = null)
		{
			return new ProblemDetails()
			{
				Status = StatusCodes.Status400BadRequest,
				Title = title,
				Detail = message,
			};
		}
	}
}

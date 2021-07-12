using AcmeCorporation.Core.Common.Utilities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AcmeCorporation.Core.Common.Middleware
{
	/// <summary>
	/// Middleware to provide a uniform way of returning errors to the front end.
	/// </summary>
	public class ProblemDetailsMiddleware
	{
		private readonly RequestDelegate _next;

		public ProblemDetailsMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception)
			{
				// could add logging here..
				await httpContext.Response.WriteAsync(JsonSerializer.Serialize(HttpUtilities.BadRequestProblemDetails("Unknown Error orcurred. staff has been contacted")));
			}
		}
	}

}

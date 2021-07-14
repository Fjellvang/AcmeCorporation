using AcmeCorporation.Core.Draw.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeCorporation.Core.Draw.Extensions
{
	public static class SubmissionResultExtensions
	{
		public static string GetErrorMessage(this SubmissionResult submissionResult)
		{
			switch (submissionResult)
			{
				case SubmissionResult.Success:
					return "";
				case SubmissionResult.InvalidSerial:
					return "Invalid serial provided";
				case SubmissionResult.SerialAlreadySubmitted:
					return "Serial Already Submitted";
				case SubmissionResult.Failure:
					return "General system error. If you already submitted with this email, please login and submit again";
				default:
					throw new ArgumentException("unexpected enum");
			}
		}
	}
}

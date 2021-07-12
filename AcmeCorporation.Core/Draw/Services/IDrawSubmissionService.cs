using AcmeCorporation.Core.Draw.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeCorporation.Core.Draw.Services
{
	public enum SubmissionResult
	{
		Success,
		InvalidSerial,
		SerialAlreadySubmitted
	}
	interface IDrawSubmissionService
	{
		Task<SubmissionResult> SubmitSerialAsync(DrawSubmissionView view);
		Task<SubmissionResult> SubmitSerialAsync(string userId, Guid Serial);
	}
}

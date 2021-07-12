using AcmeCorporation.Core.Draw.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeCorporation.Core.Draw.Services
{
	public class DrawSubmissionService : IDrawSubmissionService
	{
		private readonly IUserStore<ApplicationIdentity> userStore;

		public DrawSubmissionService(IUserStore<ApplicationIdentity> userStore)
		{
			this.userStore = userStore;
		}
		public Task<SubmissionResult> SubmitSerialAsync(DrawSubmissionView view)
		{
			throw new NotImplementedException();
		}

		public Task<SubmissionResult> SubmitSerialAsync(string userId, Guid Serial)
		{
			throw new NotImplementedException();
		}
	}
}

using AcmeCorporation.Core.Data.Models;
using AcmeCorporation.Core.Draw.Dtos;
using AcmeCorporation.Data;
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
		private readonly IUserStore<ApplicationUser> userStore;
		private readonly AcmeCorporationDbContext context;

		public DrawSubmissionService(IUserStore<ApplicationUser> userStore, AcmeCorporationDbContext context)
		{
			this.userStore = userStore;
			this.context = context;
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

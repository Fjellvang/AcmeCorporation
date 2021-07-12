using AcmeCorporation.Core.Data.Models;
using AcmeCorporation.Core.Draw.Dtos;
using AcmeCorporation.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AcmeCorporation.Core.Draw.Services
{
	public class DrawSubmissionService : IDrawSubmissionService
	{
		private readonly UserManager<ApplicationUser> userManager;
		private readonly AcmeCorporationDbContext context;
		private readonly ILogger<DrawSubmissionService> logger;

		public DrawSubmissionService(UserManager<ApplicationUser> userManager, AcmeCorporationDbContext context, ILogger<DrawSubmissionService> logger)
		{
			this.userManager = userManager;
			this.context = context;
			this.logger = logger;
		}
		public async Task<SubmissionResult> SubmitSerialAsync(DrawSubmissionView view)
		{
			var existingUser = await userManager.FindByEmailAsync(view.Email);

			if (existingUser is null)
			{
				ApplicationUser user = new ApplicationUser()
				{
					Email = view.Email,
					FirstName = view.FirstName,
					LastName = view.LastName,
				};

				var result = await userManager.CreateAsync(user, view.Password);
				if (!result.Succeeded)
				{
					logger.LogInformation("failed to create user {user}", user.Email);
					return SubmissionResult.Failure;
				}

				await context.SaveChangesAsync();
				var validSerial = await context.Serials.Where(x => 
						x.Key == view.Serial && 
						(x.UserRelation == null) // Since we're dealing with a fresh user we cannot have the serial used anywhere else.
					)
					.FirstOrDefaultAsync()
					;

				if (validSerial is null)
				{
					logger.LogInformation("Serial: {Serial}, for new user {Email} is invalid", view.Serial, user.Email);
					return SubmissionResult.InvalidSerial;
				}

				//attach serial
				var relation = new UserSerial()
				{
					Serial = validSerial,
					User = user,
					Uses = 1
				};

				context.UserSerials.Add(relation);
				await context.SaveChangesAsync();
				return SubmissionResult.Success;
			}
			//TODO:
			return SubmissionResult.Failure;
		}

		public async Task<SubmissionResult> SubmitSerialAsync(string userId, Guid serial)
		{
			var validSerial = await context.Serials
				.Include(x => x.UserRelation)
				.ThenInclude(x => x.User)
				.FirstOrDefaultAsync(x => x.Key == serial);

			if (validSerial is null || validSerial.UserRelation.UserId != userId)
			{
				return SubmissionResult.InvalidSerial;
			}

			if (validSerial.UserRelation.Uses > 1)
			{
				return SubmissionResult.SerialAlreadySubmitted;
			}

			validSerial.UserRelation.Uses++;
			await context.SaveChangesAsync();
			return SubmissionResult.Success;
		}
	}
}

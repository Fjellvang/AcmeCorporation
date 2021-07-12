using AcmeCorporation.Core.Data.Models;
using AcmeCorporation.Core.Draw.Services;
using AcmeCorporation.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace AcmeCorportation.Test
{
	public class Tests
	{
		private DrawSubmissionService service;
		private ApplicationUser applicationUser;
		private Mock<IUserStore<ApplicationUser>> store;

		[SetUp]
		public void Setup()
		{
			applicationUser = new ApplicationUser()
			{
				Email = "a@b.dk"
			};
			store = new Mock<IUserStore<ApplicationUser>>();
			store.SetupSequence(x => x.CreateAsync(applicationUser, default))
				.Returns(Task.FromResult(IdentityResult.Success))
				.Returns(Task.FromResult(IdentityResult.Failed(It.IsAny<IdentityError>())));

			var options = new DbContextOptionsBuilder<AcmeCorporationDbContext>()
				  .UseInMemoryDatabase(Guid.NewGuid().ToString())
				  .Options;
			var dbContext = new AcmeCorporationDbContext(options, default);

			dbContext.Serials.Add(new Serial() { Id = 0, Key = Guid.Empty });

			service = new DrawSubmissionService(store.Object, dbContext);
		}

		[Test]
		public void Test1()
		{
			//arrange
			//act
			//service.SubmitSerialAsync()
			//assert
		}
	}
}
using AcmeCorporation.Core.Data.Models;
using AcmeCorporation.Core.Draw.Dtos;
using AcmeCorporation.Core.Draw.Services;
using AcmeCorporation.Data;
using AcmeCorporation.Test;
using AcmeCorporation.Test.Helpers;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeCorportation.Test
{
	public class Tests
	{
		private DrawSubmissionService service;
		private Mock<UserManager<ApplicationUser>> storeMock;
		private AcmeCorporationDbContext dbContext;
		private DrawSubmissionView view;

		[SetUp]
		public void Setup()
		{
			view = new AcmeCorporation.Core.Draw.Dtos.DrawSubmissionView()
			{
				FirstName = "Don Pedro",
				LastName = "Hernandez",
				Serial = Guid.Empty,
				Email = "a@b.dk",
				Password = "123456"
			};
			applicationUser = new ApplicationUser()
			{
				Email = view.Email,
				FirstName = view.FirstName,
				LastName = view.LastName,
			};
			storeMock = MockHelpers.MockUserManager<ApplicationUser>();
			SeedInMemoryDatabase();

			storeMock.SetupSequence(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
				.ReturnsAsync(IdentityResult.Success)
				.ReturnsAsync(IdentityResult.Failed(It.IsAny<IdentityError>()));


			var logMock = new Mock<ILogger<DrawSubmissionService>>();

			service = new DrawSubmissionService(storeMock.Object, dbContext, logMock.Object);

		}

		private void SeedInMemoryDatabase()
		{
			dbContext = DbContextHelper.InMemoryDBContext();
			dbContext.Serials.Add(new Serial() { Id = 0, Key = Guid.Empty });
			dbContext.Serials.Add(new Serial() { Id = 2, Key = new Guid("63e47a81-da8b-4ac2-ba1f-c8e58feb6660") });

			dbContext.Users.Add(new ApplicationUser() { UserName = "aba@b.dk", Email = "aba@b.dk", Id = "1" });
			dbContext.UserSerials.Add(new UserSerial() { SerialId = 2, UserId = "1" });
			dbContext.SaveChanges();
		}

		[Test]
		public async Task Test_Fresh_User_Create_Called_Once()
		{
			//arrange
			//act
			await service.SubmitSerialAsync(view);
			//assert
			storeMock.Verify(x => x.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()), Times.Once);
		}
		[Test]
		public async Task Test_Fresh_User_BadId_Failure()
		{
			//arrange
			var badView = new DrawSubmissionView()
			{
				Email = "b@b.dk",
				FirstName = "Robert",
				LastName = "JohnSon",
				Password = "12345678",
				Serial = new Guid(0,1, 2, 3, 4, 5, 6, 7, 8,9,0)
			};
			//act
			var result = await service.SubmitSerialAsync(badView);
			//assert
			Assert.AreEqual(SubmissionResult.InvalidSerial, result);

		}
		[Test]
		public async Task Test_Fresh_User_Success()
		{
			//arrange
			//act
			var result = await service.SubmitSerialAsync(view);
			//assert
			Assert.AreEqual(SubmissionResult.Success, result);
		}
		[Test]
		public async Task Test_Fresh_User_Setup_Correctly()
		{
			//arrange
			//act
			await service.SubmitSerialAsync(view);
			//assert
			var user = dbContext.Users
				.Include(x => x.SerialRelations)
				.ThenInclude(x => x.Serial)
				.FirstOrDefault(x => x.Email == view.Email);

			Assert.IsNotNull(user, "User not Found");
			Assert.AreEqual(view.FirstName, user.FirstName);
			Assert.AreEqual(view.LastName, user.LastName);

		}
		[Test]
		public async Task Test_Fresh_User_Serial_Setup_Correctly()
		{
			//arrange
			await service.SubmitSerialAsync(view);
			//assert
			var user = dbContext.Users
				.Include(x => x.SerialRelations)
				.ThenInclude(x => x.Serial)
				.FirstOrDefault(x => x.Email == view.Email);

			var serial = user?.SerialRelations?.FirstOrDefault(x => x.Serial.Key == view.Serial);
			Assert.IsNotNull(serial, "Serial not found");
			Assert.AreEqual(1, serial.Uses);
		}
		[Test]
		public async Task Test_Second_serial_Uses_two()
		{
			//arrange
			await service.SubmitSerialAsync(view);
			var user = dbContext.Users
				.Include(x => x.SerialRelations)
				.ThenInclude(x => x.Serial)
				.FirstOrDefault(x => x.Email == view.Email);
			//act
			var result = await service.SubmitSerialAsync(user.Id, view.Serial);
			//assert
			Assert.AreEqual(SubmissionResult.Success, result);
			var serial = user?.SerialRelations?.FirstOrDefault(x => x.Serial.Key == view.Serial);
			Assert.AreEqual(2, serial.Uses);
		}
		[Test]
		public async Task Test_Used_Id_Fails_for_new_user()
		{

			//arrange
			await service.SubmitSerialAsync(view);
			//act
			var result = await service.SubmitSerialAsync("2", Guid.Empty);

			//assert
			Assert.AreEqual(SubmissionResult.SerialAlreadySubmitted, result);

		}
	}
}
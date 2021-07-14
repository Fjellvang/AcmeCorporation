using AcmeCorporation.Core.Common.Dtos;
using AcmeCorporation.Core.Data.Models;
using AcmeCorporation.Core.Draw;
using AcmeCorporation.Core.Draw.Dtos;
using AcmeCorporation.Core.Draw.Services;
using AcmeCorporation.Test.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeCorporation.Test
{
	public class DrawControllerTests
	{
		private Mock<ILogger<DrawController>> logger;
		private Mock<IDrawSubmissionService> submissionService;

		private Mock<SignInManager<ApplicationUser>> signInManagerMock;
		private Mock<UserManager<ApplicationUser>> userManagerMock;

		[SetUp]
		public void Setup()
		{
			logger = new Mock<ILogger<DrawController>>();
			submissionService = new Mock<IDrawSubmissionService>();
			this.userManagerMock = MockHelpers.MockUserManager<ApplicationUser>();
			this.userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
				.ReturnsAsync(new ApplicationUser())
				;
			signInManagerMock = MockHelpers.MockSigninManager<ApplicationUser>(this.userManagerMock.Object);
		}
		private DrawController GetController()
		{
			return new DrawController(
							DbContextHelper.InMemoryDBContext(), logger.Object, submissionService.Object, signInManagerMock.Object,
							this.userManagerMock.Object);
		}
		[Test]
		public async Task SubmitDraw_Returns_200_And_Calls_signin()
		{
			//arrange
			submissionService.Setup(x => x.SubmitSerialAsync(It.IsAny<DrawSubmissionView>()))
				.ReturnsAsync(SubmissionResult.Success)
				;
			var view = new DrawSubmissionView() { AboveEighteen = true, Serial = Guid.NewGuid() };
			var sut = GetController();
			//act
			var result = await sut.SubmitDraw(view) as OkResult;

			//Assert
			signInManagerMock.Verify(x => x.SignInAsync(It.IsAny<ApplicationUser>(), true, null), Times.Once);
			Assert.IsNotNull(result);
			Assert.AreEqual(200, result.StatusCode);
		}
		[Test]
		public async Task GetAllSubmissions_Returns_Empty_when_Unautorized()
		{
			//arrange
			var sut = GetController();
			sut.ControllerContext = new ControllerContext
			{
				HttpContext = new DefaultHttpContext() { User = new System.Security.Claims.ClaimsPrincipal() }
			};
			//act

			var response = (await sut.GetAllSubmissions() as OkObjectResult);
			var result = response.Value as PaginatedResult<AdminSubmissionView>;

			//assert
			Assert.IsNotNull(result);
			Assert.AreEqual(0, result.TotalResults);
			Assert.AreEqual(0, result.Results.Count());
			
		}

		[Test]
		public async Task Empty_Guid_Authorized_Returns_400()
		{
			//arrange
			var sut = GetController();
			//act
			var result = (await sut.SubmitDrawAuthorized(Guid.Empty)) as BadRequestObjectResult;
			//assert
			Assert.IsNotNull(result);
			Assert.AreEqual(400, result.StatusCode);
		}
	}
}

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

		private SignInManager<ApplicationUser> signInManager;

		[SetUp]
		public void Setup()
		{
			logger = new Mock<ILogger<DrawController>>();
			submissionService = new Mock<IDrawSubmissionService>();

			var signInManagerMock = MockHelpers.MockSigninManager<ApplicationUser>();
			//signInManagerMock.Setup(x => x.)
			signInManager = signInManagerMock.Object;
		}
		[Test]
		public async Task GetAllSubmissions_Returns_Empty_when_Unautorized()
		{
			//arrange
			var sut = new DrawController(
				DbContextHelper.InMemoryDBContext(), logger.Object, submissionService.Object, signInManager,
				MockHelpers.TestUserManager<ApplicationUser>());
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
			var sut = new DrawController(DbContextHelper.InMemoryDBContext(), logger.Object, submissionService.Object, signInManager, MockHelpers.TestUserManager<ApplicationUser>());
			//act
			var result = (await sut.SubmitDrawAuthorized(Guid.Empty)) as BadRequestObjectResult;
			//assert
			Assert.IsNotNull(result);
			Assert.AreEqual(400, result.StatusCode);
		}
	}
}

using AcmeCorporation.Core.Data.Models;
using AcmeCorporation.Core.Draw;
using AcmeCorporation.Core.Draw.Services;
using AcmeCorporation.Test.Helpers;
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

		[SetUp]
		public void Setup()
		{
			logger = new Mock<ILogger<DrawController>>();
			submissionService = new Mock<IDrawSubmissionService>();
		}

		[Test]
		public async Task Empty_Guid_Authorized_Returns_400()
		{
			//arrange
			//act
			var sut = new DrawController(DbContextHelper.InMemoryDBContext(), logger.Object, submissionService.Object, MockHelpers.MockSigninManager<ApplicationUser>().Object, MockHelpers.TestUserManager<ApplicationUser>());
			var result = (await sut.SubmitDrawAuthorized(Guid.Empty)) as BadRequestObjectResult;
			//assert
			Assert.IsNotNull(result);
			Assert.AreEqual(400, result.StatusCode);
		}
	}
}

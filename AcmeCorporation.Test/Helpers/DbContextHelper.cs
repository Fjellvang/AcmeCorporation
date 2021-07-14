using AcmeCorporation.Data;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeCorporation.Test.Helpers
{
	public static class DbContextHelper
	{
		public static AcmeCorporationDbContext InMemoryDBContext()
		{
			var options = new DbContextOptionsBuilder<AcmeCorporationDbContext>()
			  .UseInMemoryDatabase(Guid.NewGuid().ToString())
			  .Options;
			var opOptions = new OperationalStoreOptions();
			return new AcmeCorporationDbContext(options, Options.Create(opOptions));
		}
	}
}

using AcmeCorporation.Core.Data.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeCorporation.Data
{
	public class AcmeCorporationDbContext : ApiAuthorizationDbContext<ApplicationUser>
	{
		public AcmeCorporationDbContext(
			DbContextOptions options,
			IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
		{
		}

		public DbSet<Serial> Serials { get; set; }
		public DbSet<UserSerial> UserSerials { get; set; }
	}
}

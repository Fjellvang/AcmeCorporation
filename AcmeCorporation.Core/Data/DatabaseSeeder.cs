using AcmeCorporation.Core.Data.Models;
using AcmeCorporation.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeCorporation.Core.Data
{
	public class DatabaseSeeder
	{
		public DatabaseSeeder(AcmeCorporationDbContext context, UserManager<ApplicationUser> userManager)
		{
			this.context = context;
			this.userManager = userManager;
		}
		private const string GuidFilePath = "../validGuids.txt";
		private const string adminEmail = "admin@admin.dk";
		private readonly AcmeCorporationDbContext context;
		private readonly UserManager<ApplicationUser> userManager;

		public async Task SeedAsync()
		{
			var serialsExist = context.Serials.Any();
			if (!serialsExist)
			{
				if (File.Exists(GuidFilePath))
				{
					File.Delete(GuidFilePath);
				}
				using var writer = File.AppendText(GuidFilePath);
				for (int i = 0; i < 100; i++)
				{
					var guid = Guid.NewGuid();
					context.Serials.Add(new Models.Serial() { Key = guid });
					writer.WriteLine(guid);
				}
				context.SaveChanges();
			}


			var admin = await userManager.FindByEmailAsync(adminEmail);
			if (admin is null)
			{
				ApplicationUser adminUser = new ApplicationUser() { FirstName = "Admin", LastName = "Admin", UserName = adminEmail, Email = adminEmail };
				adminUser.EmailConfirmed = true;
				await userManager.CreateAsync(adminUser, "!QAZxsw2");
				await context.SaveChangesAsync();
				var result = await userManager.AddClaimAsync(adminUser, new System.Security.Claims.Claim("admin", "admin"));
			}
			//await userManager.AddToRoleAsync(admin, "admin");

		}
	}
}

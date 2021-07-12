using AcmeCorporation.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeCorporation.Core.Data
{
	public static class DatabaseSeeder
	{
		private const string GuidFilePath = "../validGuids.txt";

		public static void Seed(AcmeCorporationDbContext context)
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
		}
	}
}

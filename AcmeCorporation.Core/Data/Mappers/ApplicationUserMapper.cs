using AcmeCorporation.Core.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeCorporation.Core.Data.Mappers
{
	public class ApplicationUserMapper : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{
			builder.Property(p => p.FirstName).HasMaxLength(200).IsRequired();
			builder.Property(p => p.LastName).HasMaxLength(200).IsRequired();
		}
	}
}

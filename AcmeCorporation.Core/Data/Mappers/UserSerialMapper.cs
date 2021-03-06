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
	public class UserSerialMapper : IEntityTypeConfiguration<UserSerial>
	{
		public void Configure(EntityTypeBuilder<UserSerial> builder)
		{
			builder.HasKey(x => new { x.UserId, x.SerialId });
			builder.Property(e => e.UserId).IsRequired();
			builder.HasCheckConstraint("CK_UserSerial_Uses", "0 < [Uses] and [Uses] <= 2");
		}
	}
}

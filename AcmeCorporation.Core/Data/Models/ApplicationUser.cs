using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcmeCorporation.Core.Data.Models
{
	public class ApplicationUser : IdentityUser
	{
		public UserSerial SerialRelation { get; set; }
	}
}

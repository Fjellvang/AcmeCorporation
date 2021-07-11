using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeCorporation.Core.Data.Models
{
	public class UserSerial
	{
		public int Id { get; set; }
		public int Uses { get; set; }
		public int SerialId { get; set; }
		public string UserId { get; set; }
		public Serial Serial { get; set; }
		public ApplicationUser User { get; set; }
	}
}

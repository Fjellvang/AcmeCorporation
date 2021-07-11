using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeCorporation.Core.Data.Models
{
	public class Serial
	{
		public int Id { get; set; }
		public Guid Key { get; set; }
		public UserSerial UserRelation { get; set; }
	}
}

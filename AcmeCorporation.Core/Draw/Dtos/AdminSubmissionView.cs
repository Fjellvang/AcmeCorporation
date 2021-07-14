using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeCorporation.Core.Draw.Dtos
{
	public record AdminSubmissionView(string Email, Guid Serial, int Uses);
}

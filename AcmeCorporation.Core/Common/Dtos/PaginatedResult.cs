using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcmeCorporation.Core.Common.Dtos
{
	public class PaginatedResult<TResultType>
	{
		public PaginatedResult()
		{

		}
		public PaginatedResult(IEnumerable<TResultType> results, int totalResults)
		{
			this.Results = results;
			this.TotalResults = totalResults;
		}
		public IEnumerable<TResultType> Results { get; set; }
		public int TotalResults { get; set; }
	}
}

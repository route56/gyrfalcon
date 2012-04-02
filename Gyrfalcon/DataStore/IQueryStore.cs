using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStore
{
	public interface IQueryStore
	{
		IEnumerable<GroupedDataFormat> GetGroupedData(DateTime _startTime, DateTime _endTime);
		IEnumerable<RankedDataFormat> GetRankedData(DateTime _startTime, DateTime _endTime);
	}
}

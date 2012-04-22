using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStore.Contract
{
	public interface IQueryStore
	{
		IEnumerable<GroupedDataFormat> GetGroupedData(DateTime startTime, DateTime endTime);
		IEnumerable<RankedDataFormat> GetRankedData(DateTime startTime, DateTime endTime);
	}
}

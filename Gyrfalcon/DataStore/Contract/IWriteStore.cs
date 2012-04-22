using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStore.Contract
{
	public interface IWriteStore : IDisposable
	{
		void AddToAggregatedStore(IEnumerable<DataAtom> dataList);
	}
}

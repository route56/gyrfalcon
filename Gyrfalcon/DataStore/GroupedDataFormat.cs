using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStore
{
	public enum GroupWindowType
	{
		Hour,
		Day,
		Week
	}

	public class GroupedDataFormat
	{
		public DateTime GroupBy { get; set; }
		public GroupWindowType GroupWindow { get; set; }
		public long[] TimeSpan { get; set; }
		public string[] Activity { get; set; }
	}
}

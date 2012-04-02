using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStore
{
	public class GroupedDataFormat
	{
		public string GroupBy { get; set; }
		public long TimeSpan { get; set; }
		public string Activity { get; set; }
	}
}

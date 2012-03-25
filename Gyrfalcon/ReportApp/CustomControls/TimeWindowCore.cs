using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportApp.CustomControls
{
	public enum UberSpan
	{
		MultiYear,
		Year,
		Month,
		Week,
		Day
	}

	public class TimeWindowCore
	{
		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

		public UberSpan GetUberSpan()
		{
			throw new NotImplementedException();
		}
	}
}

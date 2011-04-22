using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessMonitor
{
	/// <summary>
	/// Data captured for a given process
	/// </summary>
	public class ProcessData
	{
		public DateTime StartTime { get; set; }

		public TimeSpan Duration { get; set; }

		public string Name { get; set; }

		public List<string> TitleList { get; set; }

	}
}

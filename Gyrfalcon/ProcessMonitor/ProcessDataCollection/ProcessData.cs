using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ProcessMonitor
{
	/// <summary>
	/// Data captured for a given process
	/// </summary>
	public abstract class ProcessData : IProcessData
	{
		public DateTime StartTime { get; set; }

		public DateTime EndTime { get; set; }

		public TimeSpan Duration
		{
			get
			{
				Debug.Assert(EndTime.CompareTo(StartTime) >= 0);
				return EndTime.Subtract(StartTime);
			}
		}

		public string Name { get; set; }


		public abstract List<IProcessData> GetSubProcessData();
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessMonitor
{
	public interface IProcessData
	{
		DateTime StartTime { get; }
		DateTime EndTime { get; }
		TimeSpan Duration { get; }
		string Name { get; }

		// Will this complicate things?
		// This can be NULL, implying no sub process
		List<IProcessData> GetSubProcessData();
	}
}

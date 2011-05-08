using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessMonitor
{
	public class VisualStudioProcessData : ProcessData
	{
		public override List<IProcessData> GetSubProcessData()
		{
			throw new NotImplementedException();
		}
	}
}

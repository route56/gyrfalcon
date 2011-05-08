using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProcessMonitor
{
	public class FirefoxProcessData : BrowserProcessData
	{
		public override List<IProcessData> GetSubProcessData()
		{
			throw new NotImplementedException();
		}
	}
}

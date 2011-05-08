using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace ProcessMonitor.ProcessDataCollection
{
	public class ProcessDataFactory
	{
		IProcessData GetProcessData(string type)
		{
			IProcessData result = null;

			switch (type.ToLower())
			{
				case "iexplorer":
					result = new InternetExplorerProcessData();
					break;

				case "firefox":
					result = new FirefoxProcessData();
					break;

				case "devenv":
					result = new VisualStudioProcessData();
					break;

				case "generic":
					result = new GenericProcessData();
					break;

				default:
					Debug.Assert(false, "Handle new process data type");
					break;
			}

			return result;
		}
	}
}

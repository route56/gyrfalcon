using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopClient.SystemServices
{
	public interface ICurrentProcess
	{
		string ProcessName { get; }
		string MainWindowTitle { get; }

		ICurrentProcess GetActiveWindowProcess();
	}
}

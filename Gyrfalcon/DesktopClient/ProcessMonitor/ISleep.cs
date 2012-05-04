using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesktopClient.ProcessMonitor
{
	public interface ISleep
	{
		void Sleep();
		void WakeUp();
		bool IsSleeping { get; }
	}
}

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DesktopClient;

namespace ClientSideIntegrationTest
{
	[TestClass]
	public class DSDCPMSA
	{
		// DesktopClient
		// DataSender
		// SystemtrayApp
		// ProcessMonitor

		[TestMethod]
		public void GetSomeDataOnAppLaunchForFakeProcess()
		{
			SystemTrayInterface sysTray = new SystemTrayInterface();
			ProcessMonitorLauncher launcher = new FakeProcessMonitorLauncher();

			launcher.Start();

			//TODOH sysTray.ProcessMonitor 
		}
	}
}

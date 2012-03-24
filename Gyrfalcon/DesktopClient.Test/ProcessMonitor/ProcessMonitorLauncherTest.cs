using DesktopClient;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using DesktopClient.ProcessMonitor;

namespace DesktopClient.Test.ProcessMonitor
{
	[TestClass()]
	public class ProcessMonitorLauncherTest
	{
		/// <summary>
		///A test for ProcessMonitorLauncher Constructor
		///</summary>
		[TestMethod()]
		public void ProcessMonitorLauncherBasicTest()
		{
			ProcessMonitorLauncher target = new ProcessMonitorLauncher();

			target.Start();
			target.Stop();
		}
	}
}
